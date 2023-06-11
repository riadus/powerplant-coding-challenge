using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

internal interface IPowerplantPowerCalculator
{
	CalculatedPower CalculateForGivenPowerplants(IEnumerable<Powerplant> powerplants, int targetLoad);
	IEnumerable<ProductionPlanForPowerplant> AdjustCalculatedPowers(CalculatedPower calculatedPower, int targetLoad);
}
