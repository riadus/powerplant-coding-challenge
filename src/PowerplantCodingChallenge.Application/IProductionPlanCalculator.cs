using PowerplantCodingChallenge.Application.Implementations;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

public interface IProductionPlanCalculator
{
	IEnumerable<ProductionPlanForPowerplant> CalculateNeededPower(Payload payload);
}
