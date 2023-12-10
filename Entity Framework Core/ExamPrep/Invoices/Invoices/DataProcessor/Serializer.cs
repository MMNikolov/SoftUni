namespace Invoices.DataProcessor
{
    using Invoices.Data;
    using Invoices.Data.Models.Enums;
    using Invoices.DataProcessor.ExportDto;
    using Microsoft.EntityFrameworkCore;
    using Newtonsoft.Json;

    public class Serializer
    {
        private static XmlHelper xmlHelper;
        public static string ExportClientsWithTheirInvoices(InvoicesContext context, DateTime date)
        {
            xmlHelper = new XmlHelper();

            var clients = context.Clients
                .Where(c => c.Invoices.Any(i => i.IssueDate > date))
                .ToArray()
                .Select(c => new ExportClientDTO
                {
                    ClientName = c.Name,
                    VatNumber = c.NumberVat,
                    InvoicesCount = c.Invoices.Count,
                    Invoices = c.Invoices
                        .OrderBy(i => i.IssueDate)
                        .ThenBy(i => i.DueDate)
                        .Select(i => new ExportInvoiceDTO
                        {
                            InvoiceNumber = i.Number,
                            InvoiceAmount = i.Amount,
                            DueDate = i.DueDate,
                            Currency = i.CurrencyType.ToString()
                        })
                        .ToArray()
                })
                .OrderByDescending(c => c.Invoices.Count())
                .ThenBy(c => c.ClientName)
                .ToArray();

            return xmlHelper.Serialize<ExportClientDTO[]>(clients, "Clients");
        }

        public static string ExportProductsWithMostClients(InvoicesContext context, int nameLength)
        {
            var products = context.Products
                .Where(p => p.ProductsClients.Any(pc => pc.Client.Name.Length >= nameLength))
                .ToArray()
                .Select(p => new 
                {
                    Name = p.Name,
                    Price = p.Price,
                    Category = p.CategoryType.ToString(),
                    Clients = p.ProductsClients
                        .Where(pc => pc.Client.Name.Length >= nameLength)
                        .ToArray()
                        .OrderBy(pc => pc.Client.Name)
                        .Select(pc => new 
                        {
                            Name = pc.Client.Name,
                            NumberVat = pc.Client.NumberVat
                        })
                        .ToArray()
                })
                .OrderByDescending(p => p.Clients.Length)
                .ThenBy(p => p.Name)
                .Take(5)
                .ToArray();

            return JsonConvert.SerializeObject(products, Formatting.Indented);
        }
    }
}