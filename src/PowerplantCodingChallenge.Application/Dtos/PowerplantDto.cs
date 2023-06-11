using System;
using System.Text.Json.Serialization;

namespace PowerplantCodingChallenge.Application.Dtos;

public class PowerplantDto
{
	[JsonPropertyName("type")]
	public string PowerplantType { get; set; } = null!;
	public string Name { get; set; } = null!;
	public float Efficiency { get; set; }

	[JsonPropertyName("pmin")]
	public float PowerMin { get; set; }

	[JsonPropertyName("pmax")]
	public float PowerMax { get; set; }
}
