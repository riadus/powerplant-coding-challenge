using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Application.Dtos;

public class PowerplantPowerDto
{
	[JsonPropertyName("name")]
	public string PowerplantName { get; set; } = null!;

	[JsonPropertyName("p")]
	public string Power { get; set; } = null!;
}