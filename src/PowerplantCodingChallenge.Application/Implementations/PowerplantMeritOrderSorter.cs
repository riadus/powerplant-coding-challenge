using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

public class PowerplantMeritOrderSorter : IPowerplantMeritOrderSorter
{
	public IOrderedEnumerable<Powerplant> SortByMeritOrder(IEnumerable<Powerplant> powerplants)
	{
		return powerplants.OrderBy(p => p.UnitPrice);
	}

	public IOrderedEnumerable<ProductionPlanForPowerplant> SortProductionsByDescreasePriority(IEnumerable<ProductionPlanForPowerplant> productionPlanForPowerplants)
	{
		return productionPlanForPowerplants.OrderByDescending(c => c.DecreasePriority);
	}
}