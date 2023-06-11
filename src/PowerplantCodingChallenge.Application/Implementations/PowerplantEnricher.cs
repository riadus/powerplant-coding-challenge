using Ardalis.GuardClauses;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Application.Implementations;

internal class PowerplantEnricher : IPowerplantEnricher
{
	private readonly IEnumerable<IPowerplantEnricherStrategy> _powerplantEnricherStrategies;

	public PowerplantEnricher(IEnumerable<IPowerplantEnricherStrategy> powerplantEnricherStrategies)
	{
		_powerplantEnricherStrategies = Guard.Against.Null(powerplantEnricherStrategies, nameof(powerplantEnricherStrategies));
	}

	public IPowerplantEnricherStrategy GetStartegy(PowerplantType powerplantType)
	{
		return _powerplantEnricherStrategies.First(x => x.PowerplantType == powerplantType);
	}
}