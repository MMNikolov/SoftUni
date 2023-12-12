namespace Cadastre.DataProcessor
{
    using Cadastre.Data;
    using Cadastre.Data.Enumerations;
    using Cadastre.Data.Models;
    using Cadastre.DataProcessor.ImportDtos;
    using Cadastre.Utilities;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public class Deserializer
    {
        private const string ErrorMessage =
            "Invalid Data!";
        private const string SuccessfullyImportedDistrict =
            "Successfully imported district - {0} with {1} properties.";
        private const string SuccessfullyImportedCitizen =
            "Succefully imported citizen - {0} {1} with {2} properties.";

        
        public static string ImportDistricts(CadastreContext dbContext, string xmlDocument)
        {
            StringBuilder sb = new StringBuilder();
            XmlSerializer serializer = new XmlSerializer(typeof(ImportDistrictDTO[]), new XmlRootAttribute("Districts"));
            using StreamReader reader = new StreamReader(xmlDocument);

            ImportDistrictDTO[] districtDTOs = (ImportDistrictDTO[])serializer.Deserialize(reader);
            List<District> districts = new List<District>();
        
            foreach (var dDTO in districtDTOs)
            {
                if (!IsValid(dDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
                if (districts.Any(d => d.Name == dDTO.Name))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }
        
                District district = new District()
                {
                    Region = dDTO.Region,
                    Name = dDTO.Name,
                    PostalCode = dDTO.PostalCode,
                };
        
                foreach (var pDTO in dDTO.Properties)
                {
                    if (!IsValid(pDTO))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (districts.Any(d => d.Properties.Any(p => p.PropertyIdentifier == pDTO.PropertyIdentifier ||
                    district.Properties.Any(dp => dp.PropertiesCitizens.Any(pc => pc.Property.PropertyIdentifier == pDTO.PropertyIdentifier)))))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
                    if (districts.Any(d => d.Properties.Any(p => p.Address == pDTO.Address ||
                    district.Properties.Any(dp => dp.PropertiesCitizens.Any(pc => pc.Property.Address == pDTO.Address)))))
                    {

                    }
        
                    DateTime dateOfAcquisition;
                    bool isDateOfAcquisition = DateTime
                        .TryParseExact(pDTO.DateOfAcquisition, "dd/MM/yyyy", CultureInfo
                        .InvariantCulture, DateTimeStyles.None, out dateOfAcquisition);
        
                    if (!isDateOfAcquisition)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }
        
                    Property property = new Property()
                    {
                        PropertyIdentifier = pDTO.PropertyIdentifier,
                        Area = pDTO.Area,
                        Details = pDTO.Details,
                        Address = pDTO.Address,
                        DateOfAcquisition = dateOfAcquisition
                    };

                    district.Properties.Add(property);
                }

                districts.Add(district);
                sb.AppendLine(string.Format(SuccessfullyImportedDistrict, district.Name, district.Properties.Count));
            }

            dbContext.AddRange(districts);
            dbContext.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportCitizens(CadastreContext dbContext, string jsonDocument)
        {
            StringBuilder sb = new StringBuilder();
            List<Citizen> validCitizens = new List<Citizen>();

            int[] validCitizensIds = dbContext.Citizens
                .Select(x => x.Id)
                .ToArray();

            var citizensDTO = JsonConvert.DeserializeObject<ImportCitizenDTO[]>(jsonDocument);

            foreach (var cDTO in citizensDTO)
            {
                if (!IsValid(cDTO))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                DateTime birthDate;
                bool isBirthDateValid = DateTime
                    .TryParseExact(cDTO.BirthDate, "dd-MM-yyyy", CultureInfo
                    .InvariantCulture, DateTimeStyles.None, out birthDate);
                
                if (!isBirthDateValid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Citizen citizen = new Citizen()
                {
                    FirstName = cDTO.FirstName,
                    LastName = cDTO.LastName,
                    BirthDate = birthDate,
                    MaritalStatus = (MaritalStatus)Enum.Parse(typeof(MaritalStatus), cDTO.MaritalStatus)
                };

                foreach (var cIds in cDTO.Properties)
                {
                    if (!validCitizensIds.Contains(cIds))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var pc = new PropertyCitizen()
                    {
                        CitizenId = cIds,
                        Citizen = citizen,
                    };

                    citizen.PropertiesCitizens.Add(pc);
                }

                validCitizens.Add(citizen);
                sb.AppendLine(String.Format(SuccessfullyImportedCitizen,
                    citizen.FirstName, citizen.LastName, 156));
            }

            dbContext.AddRange(validCitizens);
            dbContext.SaveChanges();

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
