using AutoMapper;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Mappers;

internal class PowerplantTypeToFuelTypeConverter : ITypeConverter<PowerplantType, FuelType>
{
	public FuelType Convert(PowerplantType source, FuelType destination, ResolutionContext context)
	{
		switch(source)
		{
			case PowerplantType.GasFired:
				return FuelType.Gas;
			case PowerplantType.TurboJet:
				return FuelType.Kerosine;
			case PowerplantType.WindTurbine:
				return FuelType.Wind;
			default:
				break;
		}

		throw new Exception();
	}
}
