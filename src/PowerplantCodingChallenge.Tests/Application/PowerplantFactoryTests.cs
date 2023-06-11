using FakeItEasy;
using FluentAssertions;
using PowerplantCodingChallenge.Application.Implementations;

namespace PowerplantCodingChallenge.Tests.Application;

public class PowerplantFactoryTests
{
	private readonly IPowerplantFactory _sut;
	private readonly IPowerplantEnricher _powerplantEnricher;

	public PowerplantFactoryTests()
	{
		_powerplantEnricher = A.Fake<IPowerplantEnricher>();
		_sut = new PowerplantFactory(_powerplantEnricher);
	}

	[Fact]
	public void Should_Throw_Null_Argument_Exceptions()
	{
		Action ctor = () => new PowerplantFactory(null);
		ctor.Should().Throw<ArgumentNullException>()
			.WithMessage("Value cannot be null. (Parameter 'powerplantEnricher')");
	}

	[Fact]
	public void Should_Build_Powerplants()
	{
		Powerplant gasFiredPowerplant = new()
		{
			PowerplantType = PowerplantType.GasFired
		};

		Powerplant turboJetPowerplant = new()
		{
			PowerplantType = PowerplantType.TurboJet
		};
		Payload payload = new()
		{
			Powerplants = new List<Powerplant> {
				turboJetPowerplant,
				gasFiredPowerplant
			}
		};

		var gasFiredStrategy = A.Fake<IPowerplantEnricherStrategy>();
		var turboJetStrategy = A.Fake<IPowerplantEnricherStrategy>();

		A.CallTo(() => _powerplantEnricher.GetStartegy(PowerplantType.GasFired))
			.Returns(gasFiredStrategy);

		A.CallTo(() => _powerplantEnricher.GetStartegy(PowerplantType.TurboJet))
			.Returns(turboJetStrategy);

		_sut.GetPowerplants(payload).ToList();

		A.CallTo(() => _powerplantEnricher.GetStartegy(PowerplantType.GasFired)).MustHaveHappened();
		A.CallTo(() => _powerplantEnricher.GetStartegy(PowerplantType.TurboJet)).MustHaveHappened();
		A.CallTo(() => gasFiredStrategy.PopulatePowerplant(gasFiredPowerplant, payload)).MustHaveHappened();
		A.CallTo(() => turboJetStrategy.PopulatePowerplant(turboJetPowerplant, payload)).MustHaveHappened();
	}
}
