using FluentValidation;
using FluentValidation.Validators;
using PowerplantCodingChallenge.Application.Dtos;

namespace PowerplantCodingChallenge.Application.Validations;

internal class PowerplantValidator : AbstractValidator<PowerplantDto>
{
	public PowerplantValidator()
	{
		RuleFor(x => x.Efficiency).GreaterThanOrEqualTo(0).WithMessage("The efficiency property must be defined and greater or equal to 0");
		RuleFor(x => x.PowerMax).GreaterThanOrEqualTo(0).WithMessage("The efficiency property must be defined and greater or equal to 0");
		RuleFor(x => x.PowerMin).GreaterThanOrEqualTo(0).WithMessage("The efficiency property must be defined and greater or equal to 0");
	}
}