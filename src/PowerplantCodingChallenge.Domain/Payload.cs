namespace PowerplantCodingChallenge.Domain;

public class Payload
{
	public int Load { get; set; }
	public List<Fuel> Fuels { get; set; } = new();
	public List<Powerplant> Powerplants { get; set; } = new();
}
