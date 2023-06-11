namespace PowerplantCodingChallenge.Application.Dtos;

public class PlayloadDto
{
	public int Load { get; set; }
	public FuelsDto Fuels { get; set; } = null!;
	public List<PowerplantDto> Powerplants { get; set; } = null!;
}
