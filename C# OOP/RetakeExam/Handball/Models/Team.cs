using Handball.Models.Contracts;
using Handball.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private int pointsEarned;
        private double overallRating;
        private List<IPlayer> players;

        public Team(string name)
        {
            Name = name;
            PointsEarned = 0;
            players = new List<IPlayer>();
        }

        public string Name
        {
            get
            {
                return name;
            }

            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }

                name = value;
            }
        }

        public int PointsEarned 
        { 
            get => pointsEarned; 
            private set => pointsEarned = value; 
        }

        public double OverallRating
        {
            get
            {
                if (players.Count == 0)
                {
                    return 0;
                }

                double totalRating = players.Sum(p => p.Rating);
                return Math.Round(totalRating / players.Count, 2);
            }

            private set
            {
                overallRating = value;
            }
        }

        public IReadOnlyCollection<IPlayer> Players => this.players;

        public void Draw()
        {
            PointsEarned += 1;
            foreach (var player in players)
            {
                if (player.GetType().Name == nameof(Goalkeeper))
                {
                    player.IncreaseRating();
                    break;
                }
            }
        }

        public void Lose()
        {
            foreach (var player in players)
            {
                player.DecreaseRating();
            }
        }

        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            PointsEarned += 3;

            foreach (var player in players)
            {
                player.IncreaseRating();
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new();

            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");


            sb.Append($"--Players: ");
            if (players.Count == 0)
            {
                sb.Append("none");
            }
            else 
            {
                sb.Append($"{string.Join(", ", players)}");
            }


            return sb.ToString().TrimEnd();
        }
    }
}
