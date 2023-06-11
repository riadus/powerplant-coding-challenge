using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class WindTurbinePowerplantEnricher : BasePowerplantEnricherStrategy, IPowerplantEnricherStrategy
{
	private const float GENERATED_CO2_IN_TONS = 0.3f;

	public WindTurbinePowerplantEnricher(IMapper mapper) : base(mapper)
	{
	}

	public override PowerplantType PowerplantType => PowerplantType.WindTurbine;

	public override Powerplant PopulatePowerplant(Powerplant powerplant, Payload payload)
	{
		Fuel fuel = GetFuel(powerplant, payload);

		powerplant.UnitPrice = 0;
		powerplant.PowerMax *= fuel.Price / 100; // Percentage of wind power
		powerplant.PowerMin *= fuel.Price / 100; // Percentage of wind power

		return powerplant;
	}
}

