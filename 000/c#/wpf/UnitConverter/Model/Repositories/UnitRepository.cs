using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Linq;
using UnitConverter.Model.Accessor;
using UnitConverter.Model.Entities;

namespace UnitConverter.Model.Repositories
{
    public class UnitRepository : IUnitsRepository
    {
        private readonly UnitDbContext unitDbContext;

        public UnitRepository(UnitDbContext unitDbContext)
        {
            this.unitDbContext = unitDbContext;
        }

        public Task<string> GetConversionExpression(Unit input, Unit output)
        {
            return unitDbContext.ConversionExpression.AsNoTracking()
                .Where(p => p.InUnit.Identifier == input.Identifier && p.OutUnit.Identifier == output.Identifier)
                .Select(s => s.Expression)
                .SingleAsync();
        }

        public Task<List<Unit>> InputUnitsCollection()
        {
            return unitDbContext.Units.AsNoTracking().ToListAsync();
        }

        public Task<List<Unit>> OutputUnitsCollection(Unit forUnit)
        {
            var a = unitDbContext
                .ConversionExpression
                .AsNoTracking()
                .Where(p => p.InUnitId == forUnit.Identifier)
                .Select(p => p.OutUnit);
            return a
                .ToListAsync()
            ;
        }
    }
}
