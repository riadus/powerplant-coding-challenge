namespace PowerplantCodingChallenge.Domain;

public class ProductionPlanForPowerplant : IEquatable<ProductionPlanForPowerplant>
{
	public Powerplant Powerplant { get; set; } = null!;
	public float DeliveredPower { get; set; }
	public int DecreasePriority { get; set; }
	public float UnitPrice { get; set; }

	public bool Equals(ProductionPlanForPowerplant? other)
	{
		return other?.Powerplant.Id == Powerplant.Id;
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as ProductionPlanForPowerplant);
	}

	public override int GetHashCode()
	{
		return Powerplant.GetHashCode();
	}
}
