using FakeItEasy;
using FluentAssertions;
using PowerplantCodingChallenge.Application;
using PowerplantCodingChallenge.Application.Implementations;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Tests.Application;

public class PowerplantPowerCalculatorTests
{
	private readonly IPowerplantPowerCalculator _sut;
	private readonly IPowerplantMeritOrderSorter _powerplantMeritOrderSorter;

	public PowerplantPowerCalculatorTests()
	{
		_powerplantMeritOrderSorter = A.Fake<IPowerplantMeritOrderSorter>();
		_sut = new PowerplantPowerCalculator(_powerplantMeritOrderSorter);
	}

	[Fact]
	public void Should_Throw_Null_Argument_Exceptions()
	{
		Action ctor = () => new PowerplantPowerCalculator(null);
		ctor.Should().Throw<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'powerplantMeritOrderSorter')");
	}

	[Theory]
	[InlineData(50, 100, 75, 100)]
	[InlineData(50, 100, 190, 50)]
	[InlineData(100, 200, 90, 110)]
	public void Should_Add_Power(int powerMin, int powerMax, int cumulatedLoad, int expectedDeliveredPower)
	{
		ProductionPlanForPowerplant productionPlanForPowerplant = PowerplantPowerCalculator.GetDeliveredPower(new Powerplant
		{
			PowerMin = powerMin,
			PowerMax = powerMax
		}, 200, cumulatedLoad);

		productionPlanForPowerplant.DeliveredPower.Should().Be(expectedDeliveredPower);
	}

	[Theory]
	[InlineData(50, 0, 40, 10, 0)]
	[InlineData(50, 30, 40, 30, 20)]
	public void Should_Decrease(int deliveredPower, int powerMin, int extraLoad, int newDeliveredPower, int extraLoadLeft)
	{
		ProductionPlanForPowerplant productionPlanForPowerplant = new()
		{
			DeliveredPower = deliveredPower,
			Powerplant = new Powerplant
			{
				PowerMin = powerMin
			}
		};

		float leftToDiscrase = PowerplantPowerCalculator.DecreasePower(extraLoad, productionPlanForPowerplant);

		productionPlanForPowerplant.DeliveredPower.Should().Be(newDeliveredPower);
		leftToDiscrase.Should().Be(extraLoadLeft);
	}

	[Fact]
	public void Should_Prioritise_Cheapest()
	{
		List<Powerplant> powerplants = new()
		{
			new Powerplant
			{
				UnitPrice = 10,
				PowerMax = 200
			},
			new Powerplant
			{
				UnitPrice = 1,
				PowerMin = 100,
				PowerMax = 200
			}
		};

		A.CallTo(() => _powerplantMeritOrderSorter.SortByMeritOrder(powerplants)).Returns(powerplants.OrderBy(x => x.UnitPrice));

		CalculatedPower result = _sut.CalculateForGivenPowerplants(powerplants, 190);
		result.AccumulatedPower.Should().Be(190);
		result.CalculatedPowerForPowerplants.First().UnitPrice.Should().Be(1);
		result.CalculatedPowerForPowerplants.First().DeliveredPower.Should().Be(190);

		result = _sut.CalculateForGivenPowerplants(powerplants, 230);
		result.AccumulatedPower.Should().Be(230);
		result.CalculatedPowerForPowerplants.First().UnitPrice.Should().Be(1);
		result.CalculatedPowerForPowerplants.First().DeliveredPower.Should().Be(200);

		result.CalculatedPowerForPowerplants.ElementAt(1).UnitPrice.Should().Be(10);
		result.CalculatedPowerForPowerplants.ElementAt(1).DeliveredPower.Should().Be(30);
	}

	[Fact]
	public void Should_Adjust_Least_Prio()
	{
		ProductionPlanForPowerplant cheapestOption = new()
		{
			Powerplant = new Powerplant
			{
				PowerMin = 10,
				PowerMax = 50,
			},
			DecreasePriority = 0,
			DeliveredPower = 50
		};

		ProductionPlanForPowerplant secondBestOption = new()
		{
			Powerplant = new Powerplant
			{
				PowerMin = 10,
				PowerMax = 50,
			},
			DecreasePriority = 1,
			DeliveredPower = 50
		};

		ProductionPlanForPowerplant leastInterestingOption = new()
		{
			Powerplant = new Powerplant
			{
				PowerMin = 200,
				PowerMax = 300,
			},
			DecreasePriority = 2,
			DeliveredPower = 200
		};

		var calculatedPowerForPowerplants = new List<ProductionPlanForPowerplant>
			{
				secondBestOption,
				leastInterestingOption,
				cheapestOption
			};

		CalculatedPower calculatedPower = new()
		{
			AccumulatedPower = 300,
			CalculatedPowerForPowerplants = calculatedPowerForPowerplants
		};
		A.CallTo(() => _powerplantMeritOrderSorter.SortProductionsByDescreasePriority(calculatedPowerForPowerplants)).Returns(calculatedPowerForPowerplants.OrderByDescending(x => x.DecreasePriority));


		IEnumerable<ProductionPlanForPowerplant> adjustedResults = _sut.AdjustCalculatedPowers(calculatedPower, 270);

		secondBestOption.DeliveredPower.Should().Be(20);
	}
}
