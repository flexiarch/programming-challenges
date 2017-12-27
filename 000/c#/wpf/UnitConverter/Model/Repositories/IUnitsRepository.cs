using System.Collections.Generic;
using System.Threading.Tasks;
using UnitConverter.Model.Entities;

namespace UnitConverter.Model.Repositories
{
    public interface IUnitsRepository
    {
        Task<List<Unit>> InputUnitsCollection();
        Task<List<Unit>> OutputUnitsCollection(Unit forUnit);
        Task<string> GetConversionExpression(Unit input, Unit output);
    }
}
