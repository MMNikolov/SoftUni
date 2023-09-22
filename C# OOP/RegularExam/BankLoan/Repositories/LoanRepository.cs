using BankLoan.Models;
using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class LoanRepository : IRepository<ILoan>
    {
        private List<ILoan> models;

        public LoanRepository()
        {
            this.models = new List<ILoan>();    
        }

        public IReadOnlyCollection<ILoan> Models => this.models;

        public void AddModel(ILoan model)
        {
            this.models.Add(model);
        }

        public ILoan FirstModel(string name)
        {
            foreach (var loan in models)
            {
                if (loan.GetType().Name == name)
                {
                    return loan;
                }
            }

            return null;
        }

        public bool RemoveModel(ILoan model)
        {
            return this.models.Remove(model);
        }
    }
}
