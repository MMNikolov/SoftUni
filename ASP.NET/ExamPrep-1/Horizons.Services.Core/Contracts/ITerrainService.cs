using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizons.Web.ViewModels.Destination;

namespace Horizons.Services.Core.Contracts
{
    public interface ITerrainService
    {
        Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetAllTerrainsAsync();
    }
}
