using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Horizons.Data;
using Horizons.Services.Core.Contracts;
using Horizons.Web.ViewModels;
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
        public async Task<IEnumerable<DestinationAddTerrainDropDownModel>> GetAllTerrainsAsync()
        {
            var terrains = await _context.Terrains
                .AsNoTracking()
                .Select(t => new DestinationAddTerrainDropDownModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return terrains;

        }
    }
}
