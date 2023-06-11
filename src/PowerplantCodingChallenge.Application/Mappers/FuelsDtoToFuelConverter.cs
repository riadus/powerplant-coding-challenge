using AutoMapper;
using PowerplantCodingChallenge.Domain;
using PowerplantCodingChallenge.Application.Dtos;

namespace PowerplantCodingChallenge.Application.Mappers;

internal class FuelsDtoToFuelConverter : ITypeConverter<FuelsDto, List<Fuel>>
{
	public List<Fuel> Convert(FuelsDto source, List<Fuel> destination, ResolutionContext context)
	{
		return new()
		{
			new Fuel
			{
				FuelType = FuelType.Gas,
				Price = source.Gas!.Value
			},
			new Fuel
			{
				FuelType = FuelType.Kerosine,
				Price = source.Kerosine!.Value
			},
			new Fuel
			{
				FuelType = FuelType.Co2,
				Price = source.Co2!.Value
			},
			new Fuel
			{
				FuelType = FuelType.Wind,
				Price = source.Wind!.Value
			}
		};
	}
}