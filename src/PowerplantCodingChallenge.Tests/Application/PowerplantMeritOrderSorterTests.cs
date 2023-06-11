using FluentAssertions;
using PowerplantCodingChallenge.Application.Implementations;

namespace PowerplantCodingChallenge.Tests.Application;

public class PowerplantMeritOrderSorterTests
{
	private readonly IPowerplantMeritOrderSorter _sut;

	public PowerplantMeritOrderSorterTests()
	{
		_sut = new PowerplantMeritOrderSorter();
	}

	[Fact]
	public void Should_Sort_By_MeritOrder()
	{
		IOrderedEnumerable<Powerplant> sortedPowerplants = _sut.SortByMeritOrder(new List<Powerplant>
		{
			new Powerplant
			{
				UnitPrice = 10
			},
			new Powerplant
			{
				UnitPrice = 5
			},
			new Powerplant
			{
				UnitPrice = 8
			},
		});

		sortedPowerplants.Count().Should().Be(3);
		sortedPowerplants.ElementAt(0).UnitPrice.Should().Be(5);
		sortedPowerplants.ElementAt(1).UnitPrice.Should().Be(8);
		sortedPowerplants.ElementAt(2).UnitPrice.Should().Be(10);
	}

	[Fact]
	public void Should_Sort_By_DecreasePriority()
	{
		IOrderedEnumerable<ProductionPlanForPowerplant> sortedProductionPlans = _sut.SortProductionsByDescreasePriority(new List<ProductionPlanForPowerplant>
		{
			new ProductionPlanForPowerplant
			{
				DecreasePriority = 3
			},
			new ProductionPlanForPowerplant
			{
				DecreasePriority = 1
			},
			new ProductionPlanForPowerplant
			{
				DecreasePriority = 2
			},
		});

		sortedProductionPlans.Count().Should().Be(3);
		sortedProductionPlans.ElementAt(0).DecreasePriority.Should().Be(3);
		sortedProductionPlans.ElementAt(1).DecreasePriority.Should().Be(2);
		sortedProductionPlans.ElementAt(2).DecreasePriority.Should().Be(1);
	}
}