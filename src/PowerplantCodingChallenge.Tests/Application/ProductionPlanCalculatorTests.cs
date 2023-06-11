using System;
using FakeItEasy;
using FluentAssertions;
using PowerplantCodingChallenge.Application;
using PowerplantCodingChallenge.Application.Implementations;

namespace PowerplantCodingChallenge.Tests.Application;

public class ProductionPlanCalculatorTests
{
	private readonly IProductionPlanCalculator _sut;
	private readonly IPowerplantFactory _powerplantFactory;
	private readonly IPowerplantPowerCalculator _powerplantPowerCalculator;

	public ProductionPlanCalculatorTests()
	{
		_powerplantFactory = A.Fake<IPowerplantFactory>();
		_powerplantPowerCalculator = A.Fake<IPowerplantPowerCalculator>();

		_sut = new ProductionPlanCalculator(_powerplantFactory, _powerplantPowerCalculator);
	}

	[Fact]
	public void Should_Throw_Null_Argument_Exceptions()
	{
		Action ctor = () => new ProductionPlanCalculator(null, _powerplantPowerCalculator);
		ctor.Should().Throw<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'powerplantFactory')");

		ctor = () => new ProductionPlanCalculator(_powerplantFactory, null);
		ctor.Should().Throw<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'powerplantPowerCalculator')");
	}

	[Fact]
	public void Should_Add_Missing_Powerplants()
	{
		Powerplant powerplant1 = new Powerplant { Id = Guid.NewGuid() };
		Powerplant powerplant2 = new Powerplant { Id = Guid.NewGuid() };
		ProductionPlanForPowerplant productionPlanForPowerplant = new ProductionPlanForPowerplant
		{
			Powerplant = powerplant1
		};
		IEnumerable<ProductionPlanForPowerplant> fillProductionPlan = ProductionPlanCalculator.GetMissingPowerplants(new List<ProductionPlanForPowerplant> { productionPlanForPowerplant }, new Payload
		{
			Powerplants = new List<Powerplant>
			{
				powerplant1,
				powerplant2
			}
		});

		fillProductionPlan.Should().HaveCount(1);
		fillProductionPlan.First().Powerplant.Should().Be(powerplant2);
		fillProductionPlan.First().DeliveredPower.Should().Be(0);
	}

	[Fact]
	public void Should_Calculate()
	{
		Payload payload = new() { Load = 12 };
		List<Powerplant> powerplants = new() { new Powerplant() };
		A.CallTo(() => _powerplantFactory.GetPowerplants(payload)).Returns(powerplants);

		_sut.CalculateNeededPower(payload);

		A.CallTo(() => _powerplantPowerCalculator.CalculateForGivenPowerplants(powerplants, 12)).MustHaveHappened();
	}

	[Fact]
	public void Should_Adjust()
	{
		Payload payload = new() { Load = 12 };
		List<Powerplant> powerplants = new() { new Powerplant() };
		CalculatedPower calculatedPower = new()
		{
			AccumulatedPower = 15
		};
		A.CallTo(() => _powerplantFactory.GetPowerplants(payload)).Returns(powerplants);
		A.CallTo(() => _powerplantPowerCalculator.CalculateForGivenPowerplants(powerplants, 12))
			.Returns(calculatedPower);

		_sut.CalculateNeededPower(payload);

		A.CallTo(() => _powerplantPowerCalculator.AdjustCalculatedPowers(calculatedPower, 12)).MustHaveHappened();
	}
}

