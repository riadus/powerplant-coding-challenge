using Ardalis.GuardClauses;
using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal abstract class BasePowerplantEnricherStrategy : IPowerplantEnricherStrategy
{
	private readonly IMapper _mapper;

	protected BasePowerplantEnricherStrategy(IMapper mapper)
	{
		_mapper = Guard.Against.Null(mapper, nameof(mapper));
	}

	public abstract PowerplantType PowerplantType { get; }

	public abstract Powerplant PopulatePowerplant(Powerplant powerplant, Payload payload);

	protected Fuel GetFuel(Powerplant powerplant, Payload payload)
	{
		FuelType fuelType = _mapper.Map<FuelType>(powerplant.PowerplantType);
		return payload.Fuels.First(fuel => fuel.FuelType == fuelType);
	}
}