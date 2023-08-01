using EDriveRent.Models.Contracts;
using EDriveRent.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDriveRent.Repositories
{
    public class RouteRepository : IRepository<IRoute>
    {
        private readonly List<IRoute> routes;

        public RouteRepository()
        {
            routes = new List<IRoute>();
        }

        public void AddModel(IRoute model)
        {
            this.routes.Add(model);
        }

        public IRoute FindById(string identifier)
        {
            return this.routes.FirstOrDefault(x => x.RouteId == int.Parse(identifier));
        }

        public IReadOnlyCollection<IRoute> GetAll()
        {
            return routes;
        }

        public bool RemoveById(string identifier)
        {
            var route = this.FindById(identifier);
            return routes.Remove(route);
        }
    }
}
