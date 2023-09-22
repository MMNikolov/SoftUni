using BankLoan.Models.Contracts;
using BankLoan.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankLoan.Repositories
{
    public class BankRepository : IRepository<IBank>
    {
        private List<IBank> models;

        public BankRepository()
        {
            this.models = new List<IBank>();
        }

        public IReadOnlyCollection<IBank> Models => this.models;

        public void AddModel(IBank model)
        {
            this.models.Add(model);
        }

        public IBank FirstModel(string name)
        {
            foreach (var bank in models)
            {
                if (bank.GetType().Name == name)
                {
                    return bank;
                }
            }

            return null;
        }

        public bool RemoveModel(IBank model)
        {
            return this.models.Remove(model);
        }
    }
}
