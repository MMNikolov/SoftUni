namespace Medicines.DataProcessor
{
    using Medicines.Data;
    using Medicines.Data.Models;

    public class Serializer
    {
        public static string ExportPatientsWithTheirMedicines(MedicinesContext context, string date)
        {
            throw new NotImplementedException();
        }

        public static string ExportMedicinesFromDesiredCategoryInNonStopPharmacies
            (MedicinesContext context, int medicineCategory)
        {
            var medicines = context.Medicines
                .Where(m => (int)m.Category == medicineCategory)
                .Select(m => new
                {
                    Name = m.Name,
                    Price = m.Price,
                    Pharmacy = 
                });
        }
    }
}
