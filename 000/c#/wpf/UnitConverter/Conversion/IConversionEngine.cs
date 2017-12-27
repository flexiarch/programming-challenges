using System.Threading.Tasks;
using UnitConverter.Model.Entities;

namespace UnitConverter.Conversion
{
    public interface IConversionEngine
    {
        Task<decimal> ConvertValue(decimal input, Unit from, Unit to);
    }
}
