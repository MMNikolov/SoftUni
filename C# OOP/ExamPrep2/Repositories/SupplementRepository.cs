using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class SupplementRepository : IRepository<ISupplement>
    {
        private readonly List<ISupplement> supplements;

        public SupplementRepository()
        {
            this.supplements = new List<ISupplement>();
        }

        public void AddNew(ISupplement model)
        {
            supplements.Add(model);
        }

        public ISupplement FindByStandard(int interfaceStandard)
        {
            return this.supplements.FirstOrDefault(x => x.InterfaceStandard == interfaceStandard);
        }

        public IReadOnlyCollection<ISupplement> Models()
        {
            return this.supplements.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            return this.supplements.Remove(supplements.FirstOrDefault(x => (string)x.GetType().Name == typeName));
        }
    }
}
