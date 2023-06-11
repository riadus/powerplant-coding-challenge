using Ardalis.GuardClauses;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class PowerplantFactory : IPowerplantFactory
{
	private readonly IPowerplantEnricher _powerplantEnricher;

	public PowerplantFactory(IPowerplantEnricher powerplantEnricher)
	{
		_powerplantEnricher = Guard.Against.Null(powerplantEnricher, nameof(powerplantEnricher));
	}

	public IEnumerable<Powerplant> GetPowerplants(Payload payload)
	{
		return payload.Powerplants.Select(powerplant =>
		{
			IPowerplantEnricherStrategy enricherStratergy = _powerplantEnricher.GetStartegy(powerplant.PowerplantType);
			return enricherStratergy.PopulatePowerplant(powerplant, payload);
		});
	}
}