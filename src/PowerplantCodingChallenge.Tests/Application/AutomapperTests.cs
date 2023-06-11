using AutoMapper;
using PowerplantCodingChallenge.Application.Mappers;

namespace PowerplantCodingChallenge.Tests.Application;

public class AutomapperTests
{
	[Fact]
	public void Should_Mapping_Be_Valid()
	{
		var configuration = new MapperConfiguration(cfg =>
			cfg.AddProfile<MapperProfile>()
			);

		configuration.AssertConfigurationIsValid();
	}
}

