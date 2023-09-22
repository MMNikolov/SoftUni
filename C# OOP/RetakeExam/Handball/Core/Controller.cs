using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Core
{
    public class Controller : IController
    {
        private readonly PlayerRepository players;
        private readonly TeamRepository teams;

        public Controller()
        {
            this.players = new PlayerRepository();
            this.teams = new TeamRepository();
        }

        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
            {
                return string.Format(OutputMessages.TeamAlreadyExists, name, nameof(TeamRepository));
            }
            else
            {
                Team team = new Team(name);
                this.teams.AddModel(team);
                return string.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));
            }
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != nameof(Goalkeeper) &&
                typeName != nameof(ForwardWing) &&
                typeName != nameof(CenterBack))
            {
                return string.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }
            else if (players.ExistsModel(name))
            {
                return string.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository), typeName);
            }
            else
            {
                IPlayer player;

                if (typeName == nameof(Goalkeeper))
                {
                    player = new Goalkeeper(name);
                }
                else if (typeName == nameof(CenterBack))
                {
                    player = new CenterBack(name);
                }
                else 
                {
                    player = new ForwardWing(name);
                }

                players.AddModel(player);
                return string.Format(OutputMessages.PlayerAddedSuccessfully, name);
            }
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return string.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            }
            else if (!teams.ExistsModel(teamName))
            {
                return string.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));
            }
            else if (players.GetModel(playerName).Team != null)
            {
                return string.Format(OutputMessages.PlayerAlreadySignedContract, playerName, players.GetModel(playerName).Team);
            }
            else
            {
                IPlayer player = players.GetModel(playerName);
                ITeam team = teams.GetModel(teamName);
                player.JoinTeam(teamName);
                team.SignContract(player);
                return string.Format(OutputMessages.SignContract, playerName, teamName);
            }
        }
        public string NewGame(string firstTeamName, string secondTeamName)
        {
            var firstTeam = teams.GetModel(firstTeamName);
            var secondTeam = teams.GetModel(secondTeamName);

            if (firstTeam.OverallRating > secondTeam.OverallRating)
            {
                firstTeam.Win();
                secondTeam.Lose();
                return $"Team {firstTeam.Name} wins the game over {secondTeam.Name}!";
            }
            else if (firstTeam.OverallRating < secondTeam.OverallRating)
            {
                secondTeam.Win();
                firstTeam.Lose();
                return $"Team {secondTeam.Name} wins the game over {firstTeam.Name}!";
            }

            firstTeam.Draw();

            secondTeam.Draw();

            return $"The game between {firstTeamName} and {secondTeamName} ends in a draw!";

        }
        public string PlayerStatistics(string teamName)
        {
            List<IPlayer> orderedPlayers = teams.GetModel(teamName)
                .Players.OrderByDescending(x => x.Rating)
                .ThenBy(n => n.Name).ToList();

            StringBuilder sb = new();
            sb.AppendLine($"***{teamName}***");

            foreach (var player in orderedPlayers)
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string LeagueStandings()
        {
            List<ITeam> orderedTeams = teams.Models
                .OrderByDescending(t => t.PointsEarned)
                .ThenBy(t => t.Name)
                .ToList();

            StringBuilder sb = new();

            sb.AppendLine("***League Standings***");

            foreach (var team in orderedTeams)
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }

    }
}
