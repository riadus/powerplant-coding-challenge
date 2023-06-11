using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application;

internal interface IPowerplantFactory
{
	IEnumerable<Powerplant> GetPowerplants(Payload payload);
}

