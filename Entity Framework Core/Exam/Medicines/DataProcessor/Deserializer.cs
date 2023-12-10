namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;
    using Medicines.Data.Models.Enums;
    using Medicines.DataProcessor.ImportDtos;
    using Medicines.Utilities;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid Data!";
        private const string SuccessfullyImportedPharmacy = "Successfully imported pharmacy - {0} with {1} medicines.";
        private const string SuccessfullyImportedPatient = "Successfully imported patient - {0} with {1} medicines.";

        public static string ImportPatients(MedicinesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Patient> validPatients = new List<Patient>();

            var patientsDTO = JsonConvert.DeserializeObject<ImportPatientsDTO[]>(jsonString);

            //ICollection<int> existingMedicineIds = context.Medicines
            //    .Select(m => m.Id)
            //    .ToArray();

            foreach (var pDTO in patientsDTO)
            {
                if (!IsValid(pDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var patient = new Patient()
                {
                    FullName = pDTO.FullName,
                    AgeGroup = (AgeGroup)pDTO.AgeGroup,
                    Gender = (Gender)pDTO.Gender
                };

                foreach (var medicineId in pDTO.MedicineIds.Distinct())
                {
                    //if (!existingMedicineIds.Contains(medicineId))
                    //{
                    //    sb.AppendLine(ErrorMessage);
                    //    continue;
                    //}

                    var medicines = new Medicine()
                    {
                        Id = medicineId
                    };

                    patient.PatientsMedicines.Add(new PatientMedicine
                    {
                        Medicine = medicines
                    });
                }


                validPatients.Add(patient);
                sb.AppendLine(String.Format(SuccessfullyImportedPatient,
                    patient.FullName, patient.PatientsMedicines.Count));
            }

            context.Patients.AddRange(validPatients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportPharmacies(MedicinesContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            StringBuilder sb = new StringBuilder();
            List<Pharmacy> validPharmacies = new List<Pharmacy>();

            var pharmaciesDTO = xmlHelper.Deserialize<ImportPharmacyDTO[]>(xmlString, "Pharmacies");
            foreach (var pDTO in pharmaciesDTO)
            {
                if (!IsValid(pDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var pharmacy = new Pharmacy()
                {
                    Name = pDTO.Name,
                    IsNonStop = pDTO.IsNonStop,
                    PhoneNumber = pDTO.PhoneNumber
                };

                foreach (var mDTO in pDTO.Medicines)
                {
                    if (!IsValid(mDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (mDTO.Producer == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (mDTO.ExpiryDate <= mDTO.ProductionDate)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var medicines = new Medicine()
                    {
                        Name = mDTO.Name,
                        Category = (Category)mDTO.Category,
                        Price = mDTO.Price,
                        ProductionDate = mDTO.ProductionDate,
                        ExpiryDate = mDTO.ExpiryDate,
                        Producer = mDTO.Producer
                    };

                    pharmacy.Medicines.Add(medicines);
                }

                validPharmacies.Add(pharmacy);
                sb.AppendLine(string.Format(SuccessfullyImportedPharmacy, pharmacy.Name, pharmacy.Medicines.Count));
            }

            context.Pharmacies.AddRange(validPharmacies);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    }
}
