namespace PowerplantCodingChallenge.Domain;

public class CalculatedPower
{
	public List<ProductionPlanForPowerplant> CalculatedPowerForPowerplants { get; set; } = new();
	public float AccumulatedPower { get; set; }
}