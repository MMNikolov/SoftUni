﻿namespace Trucks.DataProcessor
{
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using Castle.Core.Internal;
    using Data;
    using Newtonsoft.Json;
    using Trucks.Data.Models;
    using Trucks.Data.Models.Enums;
    using Trucks.DataProcessor.ImportDto;
    using Trucks.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedDespatcher
            = "Successfully imported despatcher - {0} with {1} trucks.";

        private const string SuccessfullyImportedClient
            = "Successfully imported client - {0} with {1} trucks.";

        public static string ImportDespatcher(TrucksContext context, string xmlString)
        {
            XmlHelper xmlHelper = new XmlHelper();
            StringBuilder sb = new StringBuilder();
            List<Despatcher> despatchers = new List<Despatcher>();

            ImportDespatcherDTO[] despatchersDto =
                xmlHelper.Deserialize<ImportDespatcherDTO[]>(xmlString, "Despatchers");

            foreach (var dto in despatchersDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                string position = dto.Position;
                bool isPositionInvalid = string.IsNullOrEmpty(position);

                if (isPositionInvalid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                var despatcher = new Despatcher()
                {
                    Name = dto.Name,
                    Position = position,
                };


                foreach (var dtoTruck in dto.Trucks)
                {
                    if (!IsValid(dtoTruck))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    var truck = new Truck()
                    {
                        RegistrationNumber = dtoTruck.RegistrationNumber,
                        VinNumber = dtoTruck.VinNumber,
                        TankCapacity = dtoTruck.TankCapacity,
                        CargoCapacity = dtoTruck.CargoCapacity,
                        CategoryType = (CategoryType)dtoTruck.CategoryType,
                        MakeType = (MakeType)dtoTruck.MakeType,
                    };

                    despatcher.Trucks.Add(truck);
                }
                despatchers.Add(despatcher);
                sb.AppendLine(string.Format(SuccessfullyImportedDespatcher, despatcher.Name, despatcher.Trucks.Count));
            }
            context.Despatchers.AddRange(despatchers);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }
        public static string ImportClient(TrucksContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Client> clients = new List<Client>();

            var clientsDto = JsonConvert.DeserializeObject<ImportClientDTO[]>(jsonString);

            foreach (var dto in clientsDto)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Client client = new Client()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                    Type = dto.Type,
                };

                if (client.Type == "usual")
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (var truck in dto.Trucks.Distinct())
                {
                    Truck tr = context.Trucks.Find(truck);
                    if (tr == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    client.ClientsTrucks.Add(new ClientTruck()
                    {
                        Truck = tr
                    });
                }

                clients.Add(client);
                sb.AppendLine(String.Format(SuccessfullyImportedClient,
                    client.Name, client.ClientsTrucks.Count));
            }

            context.Clients.AddRange(clients);
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