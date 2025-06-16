using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizons.Data;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels.Destination;
using Microsoft.EntityFrameworkCore;

namespace Horizons.Services.Core
{
    public class TerrainService : ITerrainService
    {
        private readonly ApplicationDbContext _context;

        public TerrainService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AddDestinationTerrainDropDownModel>> GetAllTerrainsAsync()
        {
            return await _context.Terrains
                .AsNoTracking()
                .Select(t => new AddDestinationTerrainDropDownModel()
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToArrayAsync();
        }

    }
}
