using System.Threading.Tasks;
using DynamicExpresso;
using UnitConverter.Model.Entities;
using UnitConverter.Model.Repositories;

namespace UnitConverter.Conversion
{
    public class ConversionEngine : IConversionEngine
    {
        private readonly IUnitsRepository repository;

        public ConversionEngine(IUnitsRepository repository)
        {
            this.repository = repository;
        }

        public async Task<decimal> ConvertValue(decimal input, Unit from, Unit to)
        {
            string expression = await repository.GetConversionExpression(from, to);
            var inter = new Interpreter(InterpreterOptions.PrimitiveTypes);
            Lambda parsedExpression = inter.Parse(expression, new Parameter("x", typeof(decimal)));
            var result = parsedExpression.Invoke(input) as decimal?;
            return result.Value;
        }
    }
}
