namespace PowerplantCodingChallenge.Domain;
public class Powerplant
{
	public Guid Id { get; set; }
	public PowerplantType PowerplantType { get; set; }
	public string Name { get; set; } = null!;
	public float Efficiency { get; set; }
	public float PowerMin { get; set; }
	public float PowerMax { get; set; }

	public float MinimumPrice { get; set; }
	public float MaximumPrice { get; set; }
	public float UnitPrice { get; set; }
}