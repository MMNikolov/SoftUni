namespace Invoices.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Invoices.Data;
    using Invoices.Data.Models;
    using Invoices.DataProcessor.ImportDto;
    using Newtonsoft.Json;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedClients
            = "Successfully imported client {0}.";

        private const string SuccessfullyImportedInvoices
            = "Successfully imported invoice with number {0}.";

        private const string SuccessfullyImportedProducts
            = "Successfully imported product - {0} with {1} clients.";


        public static string ImportClients(InvoicesContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            List<Client> validClients = new List<Client>();

            var clientsDto = xmlHelper.Deserialize<ImportClientDTO[]>(xmlString, "Clients");

            foreach (var client in clientsDto)
            {
                if (!IsValid(client))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client clients = new Client()
                {
                    Name = client.Name,
                    NumberVat = client.NumberVat
                };

                foreach (var address in client.Addresses)
                {
                    if (!IsValid(address))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Address addresses = new Address()
                    {
                        StreetName = address.StreetName,
                        StreetNumber = address.StreetNumber,
                        PostCode = address.PostCode,
                        City = address.City,
                        Country = address.Country
                    };

                    clients.Addresses.Add(addresses);
                }

                validClients.Add(clients);
                sb.AppendLine(String.Format(SuccessfullyImportedClients, client.Name));
            }

            context.Clients.AddRange(validClients);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }


        public static string ImportInvoices(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Invoice> validInvoices = new List<Invoice>();

            var invoicesDto = JsonConvert.DeserializeObject<ImportInvoiceDTO[]>(jsonString);

            foreach (var dto in invoicesDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                if (dto.DueDate == DateTime.ParseExact("01/01/0001", "dd/MM/yyyy",
                    CultureInfo.InvariantCulture) ||
                    dto.IssueDate == DateTime.ParseExact("01/01/0001", "dd/MM/yyyy",
                    CultureInfo.InvariantCulture))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var invoices = new Invoice()
                {
                    Number = dto.Number,
                    IssueDate = dto.IssueDate,
                    DueDate = dto.DueDate,
                    Amount = dto.Amount,
                    CurrencyType = dto.CurrencyType,
                    ClientId = dto.ClientId
                };

                if (dto.IssueDate > dto.DueDate)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                sb.AppendLine(String.Format(SuccessfullyImportedInvoices, dto.Number));
                validInvoices.Add(invoices);
            }

            context.Invoices.AddRange(validInvoices);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportProducts(InvoicesContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Product> validproducts = new List<Product>();

            var productsDto = JsonConvert.DeserializeObject<ImportProductDTO[]>(jsonString);

            foreach (var dto in productsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var products = new Product()
                {
                    Name = dto.Name,
                    Price = dto.Price,
                    CategoryType = dto.CategoryType,
                };

                foreach (var client in dto.Clients.Distinct())
                {
                    Client c = context.Clients.Find(client);
                    if (c == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    products.ProductsClients.Add(new ProductClient()
                    {
                        Client = c
                    });
                }

                validproducts.Add(products);
                sb.AppendLine(String.Format(SuccessfullyImportedProducts, products.Name, products.ProductsClients.Count));
            }

            context.Products.AddRange(validproducts);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static bool IsValid(object dto)
        {
            var validationContext = new ValidationContext(dto);
            var validationResult = new List<ValidationResult>();

            return Validator.TryValidateObject(dto, validationContext, validationResult, true);
        }
    } 
}
