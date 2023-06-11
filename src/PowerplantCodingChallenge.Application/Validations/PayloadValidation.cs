using System;
using FluentValidation;
using PowerplantCodingChallenge.Application.Dtos;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Validations
{
	public class PayloadValidation : AbstractValidator<PlayloadDto>
	{
		public PayloadValidation()
		{
			RuleFor(x => x.Fuels).NotNull().WithMessage("The list of fuels is mandatory");
			RuleFor(x => x.Fuels.Co2).NotNull().WithMessage("The 'co2(euro/ton)' property of fuels is mandatory");
			RuleFor(x => x.Fuels.Gas).NotNull().WithMessage("The 'gas(euro/MWh)' property of fuels is mandatory");
			RuleFor(x => x.Fuels.Kerosine).NotNull().WithMessage("The 'kerosine(euro/MWh)' property of fuels is mandatory");
			RuleFor(x => x.Fuels.Wind).NotNull().WithMessage("The 'wind(%)' property of fuels is mandatory");
			RuleFor(x => x.Load).GreaterThan(0).WithMessage("The Load property must be defined and stricly greater than 0");
			RuleForEach(x => x.Powerplants).SetValidator(new PowerplantValidator());
			RuleFor(x => x.Powerplants).Must(ProvidedPowerPlantMustReachLoad).WithMessage("The provided powerplants cannot reach the load defined");
		}

		private bool ProvidedPowerPlantMustReachLoad(PlayloadDto payloadDto, List<PowerplantDto> powerplantDtos)
		{
			return powerplantDtos.Sum(x => x.PowerMax) >= payloadDto.Load;
		}
	}
}

