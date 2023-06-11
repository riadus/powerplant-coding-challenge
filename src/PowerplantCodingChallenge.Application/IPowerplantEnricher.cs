using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

internal interface IPowerplantEnricher
{
	IPowerplantEnricherStrategy GetStartegy(PowerplantType powerplantType);
}

