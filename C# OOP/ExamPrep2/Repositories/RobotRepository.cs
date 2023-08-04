using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots;

        public RobotRepository()
        {
            this.robots = new List<IRobot>();
        }

        public void AddNew(IRobot model)
        {
            this.robots.Add(model);
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return this.robots.FirstOrDefault(x => x.InterfaceStandards.Any(y => y == interfaceStandard));
        }

        public IReadOnlyCollection<IRobot> Models()
        {
            return this.robots.AsReadOnly();
        }

        public bool RemoveByName(string typeName)
        {
            return this.robots.Remove(this.robots.FirstOrDefault(x => x.GetType().Name == typeName));
        }
    }
}
