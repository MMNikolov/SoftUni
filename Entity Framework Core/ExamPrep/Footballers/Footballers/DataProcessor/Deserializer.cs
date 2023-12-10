namespace Footballers.DataProcessor
{
    using Footballers.Data;
    using Footballers.Data.Models;
    using Footballers.Data.Models.Enums;
    using Footballers.DataProcessor.ImportDto;
    using Newtonsoft.Json;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Text;
    using Trucks.Utilities;

    public class Deserializer
    {
        private const string ErrorMessage = "Invalid data!";

        private const string SuccessfullyImportedCoach
            = "Successfully imported coach - {0} with {1} footballers.";

        private const string SuccessfullyImportedTeam
            = "Successfully imported team - {0} with {1} footballers.";

        public static string ImportCoaches(FootballersContext context, string xmlString)
        {
            StringBuilder sb = new StringBuilder();
            XmlHelper xmlHelper = new XmlHelper();
            List<Coach> validCoaches = new List<Coach>();

            var coaches = xmlHelper.Deserialize<ImportCoachDTO[]>(xmlString, "Coaches");
            foreach (ImportCoachDTO dto in coaches)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                bool isNationalityInvalid = string.IsNullOrEmpty(dto.Nationality);

                if (isNationalityInvalid)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Coach coach = new Coach()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                };

                foreach (ImportFootballerDTO footballerDto in dto.Footballers)
                {
                    if (!IsValid(footballerDto))
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime contractStartInfo;
                    bool IsContractStartInfoValid = DateTime.TryParseExact(footballerDto.ContractStartDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out contractStartInfo);

                    if (!IsContractStartInfoValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    DateTime contractEndInfo;
                    bool IsContractEndInfoValid = DateTime.TryParseExact(footballerDto.ContractEndDate,
                        "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out contractEndInfo);

                    if (!IsContractEndInfoValid)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    if (contractStartInfo >= contractEndInfo)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    Footballer f = new Footballer()
                    {
                        Name = footballerDto.Name,
                        ContractStartDate = contractStartInfo,
                        ContractEndDate = contractEndInfo,
                        BestSkillType = (BestSkillType)footballerDto.BestSkillType,
                        PositionType = (PositionType)footballerDto.PositionType,
                    };

                    coach.Footballers.Add(f);
                }

                validCoaches.Add(coach);
                sb.AppendLine(String.Format(SuccessfullyImportedCoach, coach.Name, coach.Footballers.Count));
            }

            context.SaveChanges();
            context.Coaches.AddRange(validCoaches);

            return sb.ToString().TrimEnd();
        }

        public static string ImportTeams(FootballersContext context, string jsonString)
        {
            StringBuilder sb = new StringBuilder();
            List<Team> validTeams = new List<Team>();

            var teamDTO = JsonConvert.DeserializeObject<ImportTeamsDTO[]>(jsonString);

            foreach (ImportTeamsDTO dto in teamDTO)
            {
                if (!IsValid(dto))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                Team t = new Team()
                {
                    Name = dto.Name,
                    Nationality = dto.Nationality,
                    Trophies = dto.Trophies
                };

                if (t.Trophies == 0)
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                foreach (int footballerId in dto.Footballers.Distinct())
                {
                    Footballer f = context.Footballers.Find(footballerId);
                    if (f == null)
                    {
                        sb.AppendLine(ErrorMessage);
                        continue;
                    }

                    t.TeamsFootballers.Add(new TeamFootballer()
                    {
                        Footballer = f
                    });
                }

                validTeams.Add(t);
                sb.AppendLine(string.Format
                    (SuccessfullyImportedTeam, t.Name, t.TeamsFootballers.Count));
            }

            context.Teams.AddRange(validTeams);
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
