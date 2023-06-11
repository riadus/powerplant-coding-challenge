using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Application.Dtos;

public class FuelsDto
{
	[JsonPropertyName("gas(euro/MWh)")]
	public float? Gas { get; set; }

	[JsonPropertyName("kerosine(euro/MWh)")]
	public float? Kerosine { get; set; }

	[JsonPropertyName("co2(euro/ton)")]
	public float? Co2 { get; set; }

	[JsonPropertyName("wind(%)")]
	public float? Wind { get; set; }
}
