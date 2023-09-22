using BankLoan.Models.Contracts;
using BankLoan.Repositories;
using BankLoan.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Models
{
    public abstract class Bank : IBank
    {
        private string name;
        private int capacity
        private readonly List<IClient> clients;

        public Bank(string name, int capacity)
        {
            Name = name;
            Capacity = capacity;
            this.loans = new LoanRepository();
            this.clients = new List<IClient>();
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.BankNameNullOrWhiteSpace);
                }

                name = value;
            }
        }

        public int Capacity
        {
            get => capacity;
            private set => capacity = value;
        }

        public IReadOnlyCollection<ILoan> Loans { get; private set; }

        public IReadOnlyCollection<IClient> Clients { get; private set; }

        public void AddClient(IClient Client)
        {
            if (clients.Count >= capacity)
            {
                throw new ArgumentException("Not enough capacity for this client.");
            }

            clients.Add(Client);
        }

        public void AddLoan(ILoan loan)
        {
            this.loans.Add(loan);
        }

        public string GetStatistics()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Name: {Name}, Type: {nameof(Bank)}");
            
            if (clients.Count > 0)
            {
                sb.Append("Clients: ");

                foreach (var client in clients)
                {
                    sb.Append(client.Name).Append(", ");
                }

                sb.Length -= 2; 

                sb.AppendLine();
            }
            else
            {
                sb.AppendLine("Clients: none");
            }

            sb.AppendLine($"Loans: {loans.Count}, Sum of Rates: {SumRates()}");

            return sb.ToString().TrimEnd();

        }

        public void RemoveClient(IClient Client)
        {
            this.clients.Remove(Client);
        }

        public double SumRates()
        {
            double sumRates = 0.0;

            foreach (var loan in loans)
            {
                sumRates += loan.InterestRate;
            }

            return sumRates;
        }
    }
}
