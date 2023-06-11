﻿using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

public interface IPowerplantMeritOrderSorter
{
	IOrderedEnumerable<Powerplant> SortByMeritOrder(IEnumerable<Powerplant> powerplants);
	IOrderedEnumerable<ProductionPlanForPowerplant> SortProductionsByDescreasePriority(IEnumerable<ProductionPlanForPowerplant> productionPlanForPowerplants);
}