using System;
using AutoMapper;
using PowerplantCodingChallenge.Domain;
using PowerplantCodingChallenge.Application.Dtos;

namespace PowerplantCodingChallenge.Application.Mappers;

public class MapperProfile : Profile
{
	public MapperProfile()
	{
		CreateMap<PowerplantType, FuelType>().ConvertUsing(new PowerplantTypeToFuelTypeConverter());
		CreateMap<PowerplantDto, Powerplant>()
			.ForMember(dest => dest.Id, opt => opt.MapFrom(_ => Guid.NewGuid()))
			.ForMember(dest => dest.UnitPrice, opt => opt.Ignore());
		CreateMap<FuelsDto, List<Fuel>>().ConvertUsing(new FuelsDtoToFuelConverter());
		CreateMap<PlayloadDto, Payload>();
		CreateMap<ProductionPlanForPowerplant, PowerplantPowerDto>()
			.ForMember(dest => dest.PowerplantName, opt => opt.MapFrom(src => src.Powerplant.Name))
			.ForMember(dest => dest.Power, opt => opt.MapFrom(src => Math.Round(src.DeliveredPower, 1)));
	}
}