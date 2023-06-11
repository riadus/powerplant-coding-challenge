using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class GasFiredPowerplantEnricher : BasePowerplantEnricherStrategy, IPowerplantEnricherStrategy
{
	private const float GENERATED_CO2_PER_MWH_IN_TONS = 0.3f;

	public GasFiredPowerplantEnricher(IMapper mapper) : base(mapper)
	{
	}

	public override PowerplantType PowerplantType => PowerplantType.GasFired;

	public override Powerplant PopulatePowerplant(Powerplant powerplant, Payload payload)
	{
		Fuel fuel = GetFuel(powerplant, payload);

		float co2Price = payload.Fuels.First(x => x.FuelType == FuelType.Co2).Price;
		float averagePrice = fuel.Price / powerplant.Efficiency + GENERATED_CO2_PER_MWH_IN_TONS * co2Price;
		powerplant.MinimumPrice = powerplant.PowerMin * averagePrice;
		powerplant.MaximumPrice = powerplant.PowerMax * averagePrice;
		powerplant.UnitPrice = averagePrice;
		return powerplant;
	}
}

