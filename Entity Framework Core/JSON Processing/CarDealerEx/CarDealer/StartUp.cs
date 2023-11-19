using AutoMapper;
using CarDealer.Data;
using CarDealer.DTOs;
using CarDealer.Models;
using Newtonsoft.Json;
using System.IO;

namespace CarDealer
{
    public class StartUp
    {
        public static void Main()
        {
            CarDealerContext context = new CarDealerContext();

            // 9. Import Suppliers
            //string supplierJson = File.ReadAllText("../../../Datasets/suppliers.json");
            //Console.WriteLine(ImportSuppliers(context, supplierJson));

            // 10. Import parts
            string partsJson = File.ReadAllText("../../../Datasets/parts.json");
            Console.WriteLine(ImportParts(context, partsJson));
        }

        // 9. Import Suppliers
        public static string ImportSuppliers(CarDealerContext context, string inputJson)
        {
            var supplierJson = JsonConvert.DeserializeObject<Supplier[]>(inputJson);

            context.Suppliers.AddRange(supplierJson);
            context.SaveChanges();

            return $"Successfully imported {supplierJson.Count()}";
        }

        // 10. Import parts
        public static string ImportParts(CarDealerContext context, string inputJson)
        {
            var PartsJson = JsonConvert.DeserializeObject<Part[]>(inputJson);

            int[] supplierIds = context.Suppliers
                .Select(x => x.Id)
                .ToArray();

            Part[] partsWithvalidSuppliers = context.Parts
                .Where(p => supplierIds.Contains(p.SupplierId)).ToArray();

            context.Parts.AddRange(partsWithvalidSuppliers);
            context.SaveChanges();

            return $"Successfully imported {partsWithvalidSuppliers.Count()}";
        }
    }
}