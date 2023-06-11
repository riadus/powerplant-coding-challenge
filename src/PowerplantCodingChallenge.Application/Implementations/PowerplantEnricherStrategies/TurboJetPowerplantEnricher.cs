using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class TurboJetPowerplantEnricher : BasePowerplantEnricherStrategy, IPowerplantEnricherStrategy
{
	public TurboJetPowerplantEnricher(IMapper mapper) : base(mapper)
	{
	}

	public override PowerplantType PowerplantType => PowerplantType.TurboJet;

	public override Powerplant PopulatePowerplant(Powerplant powerplant, Payload payload)
	{
		Fuel fuel = GetFuel(powerplant, payload);

		powerplant.MinimumPrice = powerplant.PowerMin * fuel.Price / powerplant.Efficiency;
		powerplant.MaximumPrice = powerplant.PowerMax * fuel.Price / powerplant.Efficiency;
		powerplant.UnitPrice = fuel.Price / powerplant.Efficiency;

		return powerplant;
	}
}

