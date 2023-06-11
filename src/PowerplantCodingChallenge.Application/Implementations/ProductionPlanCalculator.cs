
using Ardalis.GuardClauses;
using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class ProductionPlanCalculator : IProductionPlanCalculator
{
	private readonly IPowerplantFactory _powerplantFactory;
	private readonly IPowerplantPowerCalculator _powerplantPowerCalculator;

	public ProductionPlanCalculator(IPowerplantFactory powerplantFactory,
			IPowerplantPowerCalculator powerplantPowerCalculator)
	{
		_powerplantFactory = Guard.Against.Null(powerplantFactory, nameof(powerplantFactory));
		_powerplantPowerCalculator = Guard.Against.Null(powerplantPowerCalculator, nameof(powerplantPowerCalculator));
	}

	internal IEnumerable<ProductionPlanForPowerplant> CalculateNeededPowerInternal(IEnumerable<Powerplant> powerplants, int load)
	{
		CalculatedPower calcualtedPower = _powerplantPowerCalculator.CalculateForGivenPowerplants(powerplants, load);

		if (calcualtedPower.AccumulatedPower > load)
		{
			return _powerplantPowerCalculator.AdjustCalculatedPowers(calcualtedPower, load);
		}

		return calcualtedPower.CalculatedPowerForPowerplants;
	}

	public IEnumerable<ProductionPlanForPowerplant> CalculateNeededPower(Payload payload)
	{
		IEnumerable<Powerplant> powerplants = _powerplantFactory.GetPowerplants(payload);

		List<ProductionPlanForPowerplant> productionPlans = CalculateNeededPowerInternal(powerplants, payload.Load).ToList();

		productionPlans.AddRange(GetMissingPowerplants(productionPlans, payload));

		return productionPlans;
	}

	internal static IEnumerable<ProductionPlanForPowerplant> GetMissingPowerplants(List<ProductionPlanForPowerplant> productionPlans, Payload payload)
	{
		IEnumerable<Powerplant> missingPowerplants = payload.Powerplants.Except(productionPlans.Select(x => x.Powerplant));
		return missingPowerplants.Select(missingPowerplant => new ProductionPlanForPowerplant
		{
			Powerplant = missingPowerplant,
			DeliveredPower = 0
		});
	}
}