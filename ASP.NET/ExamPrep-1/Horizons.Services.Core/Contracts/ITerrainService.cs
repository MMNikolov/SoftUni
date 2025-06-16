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
        /// <summary>
        /// Gets all terrains.
        /// </summary>
        /// <returns>A collection of terrain names.</returns>
        Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetAllTerrainsAsync();
        /// <summary>
        /// Gets the terrain name by its ID.
        /// </summary>
        /// <param name="terrainId">The ID of the terrain.</param>
        /// <returns>The name of the terrain.</returns>
        //Task<string> GetTerrainNameByIdAsync(int terrainId);
    }
}
