using Ardalis.GuardClauses;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class PowerplantPowerCalculator : IPowerplantPowerCalculator
{
	private readonly IPowerplantMeritOrderSorter _powerplantMeritOrderSorter;

	public PowerplantPowerCalculator(IPowerplantMeritOrderSorter powerplantMeritOrderSorter)
	{
		_powerplantMeritOrderSorter = Guard.Against.Null(powerplantMeritOrderSorter, nameof(powerplantMeritOrderSorter));
	}

	public IEnumerable<ProductionPlanForPowerplant> AdjustCalculatedPowers(CalculatedPower calculatedPower, int targetLoad)
	{
		IOrderedEnumerable<ProductionPlanForPowerplant> orderedCalculatedPower = _powerplantMeritOrderSorter.SortProductionsByDescreasePriority(calculatedPower.CalculatedPowerForPowerplants);
		float powerToDecrease = calculatedPower.AccumulatedPower - targetLoad;

		List<ProductionPlanForPowerplant> adjustedPowers = new();

		foreach(ProductionPlanForPowerplant calculatedPowerForPowerPlant in orderedCalculatedPower)
		{
			if(powerToDecrease > 0 && calculatedPowerForPowerPlant.DeliveredPower > calculatedPowerForPowerPlant.Powerplant.PowerMin)
			{
				powerToDecrease = DecreasePower(powerToDecrease, calculatedPowerForPowerPlant);
			}
			adjustedPowers.Insert(0, calculatedPowerForPowerPlant);
		}

		return adjustedPowers;
	}

	internal static float DecreasePower(float powerToDecrease, ProductionPlanForPowerplant calculatedPowerForPowerplant)
	{
		if(calculatedPowerForPowerplant.DeliveredPower - powerToDecrease > calculatedPowerForPowerplant.Powerplant.PowerMin)
		{
			calculatedPowerForPowerplant.DeliveredPower -= powerToDecrease;
			return 0;
		}

		float reducedPower = calculatedPowerForPowerplant.DeliveredPower - calculatedPowerForPowerplant.Powerplant.PowerMin;
		calculatedPowerForPowerplant.DeliveredPower = calculatedPowerForPowerplant.Powerplant.PowerMin;

		return powerToDecrease - reducedPower;
	}

	internal static ProductionPlanForPowerplant GetDeliveredPower(Powerplant powerplant, int targetLoad, float accumulatedPower)
	{
		ProductionPlanForPowerplant calculatedPower = new()
		{
			Powerplant = powerplant,
			UnitPrice = powerplant.UnitPrice
		};

		if(powerplant.PowerMax + accumulatedPower <= targetLoad)
		{
			calculatedPower.DeliveredPower = powerplant.PowerMax;
		}
		else
		{
			float powerToAdd = targetLoad - accumulatedPower;
			if(powerToAdd < powerplant.PowerMin)
			{
				powerToAdd = powerplant.PowerMin;
			}
			calculatedPower.DeliveredPower = powerToAdd;
		}

		return calculatedPower;
	}

	public CalculatedPower CalculateForGivenPowerplants(IEnumerable<Powerplant> powerplants, int targetLoad)
	{
		IOrderedEnumerable<Powerplant> orderedPrices = _powerplantMeritOrderSorter.SortByMeritOrder(powerplants);

		float accumulatedPower = 0;
		int priority = 0;

		List<ProductionPlanForPowerplant> neededPowerPlants = new();
		foreach (Powerplant powerplant in orderedPrices)
		{
			ProductionPlanForPowerplant deliveredPower = GetDeliveredPower(powerplant, targetLoad, accumulatedPower);
			deliveredPower.DecreasePriority = priority++;

			neededPowerPlants.Add(deliveredPower);
			accumulatedPower += deliveredPower.DeliveredPower;

			if (accumulatedPower >= targetLoad)
			{
				break;
			}
		}

		return new CalculatedPower
		{
			AccumulatedPower = accumulatedPower,
			CalculatedPowerForPowerplants = neededPowerPlants,
		};
	}
}
