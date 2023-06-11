using FakeItEasy;
using FluentAssertions;
using PowerplantCodingChallenge.Application.Implementations;

namespace PowerplantCodingChallenge.Tests.Application;

public class PowerplantEnricherTests
{
	[Fact]
	public void Should_Throw_Null_Argument_Exceptions()
	{
		Action ctor = () => new PowerplantEnricher(null);
		ctor.Should().Throw<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'powerplantEnricherStrategies')");
	}

	[Fact]
	public void Should_Get_Strategy()
	{
		IPowerplantEnricherStrategy gasPowerplantEnricher = A.Fake<IPowerplantEnricherStrategy>();
		IPowerplantEnricherStrategy windTurbinePowerplantEnricher = A.Dummy<IPowerplantEnricherStrategy>();
		A.CallTo(() => gasPowerplantEnricher.PowerplantType).Returns(PowerplantType.GasFired);

		IPowerplantEnricher sut = new PowerplantEnricher(new List<IPowerplantEnricherStrategy>
		{
			gasPowerplantEnricher,
			windTurbinePowerplantEnricher
		});

		IPowerplantEnricherStrategy strategy = sut.GetStartegy(PowerplantType.GasFired);

		strategy.Should().Be(gasPowerplantEnricher);
	}
}