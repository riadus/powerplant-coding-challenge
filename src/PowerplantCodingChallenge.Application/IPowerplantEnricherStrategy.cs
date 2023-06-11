using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

internal interface IPowerplantEnricherStrategy
{
	PowerplantType PowerplantType { get; }
	Powerplant PopulatePowerplant(Powerplant powerplant, Payload payload);
}

