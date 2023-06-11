
using AutoMapper;
using FluentAssertions;
using PowerplantCodingChallenge.Application.Implementations;
using PowerplantCodingChallenge.Application.Mappers;
using PowerplantCodingChallenge.Domain;

namespace PowerplantCodingChallenge.Tests;

public class PowerCalculatorIntegrationTests
{
    private readonly IProductionPlanCalculator _sut;

	public PowerCalculatorIntegrationTests()
	{
		MapperConfiguration configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
		IMapper mapper = configuration.CreateMapper();
		_sut = new ProductionPlanCalculator(new PowerplantFactory( new PowerplantEnricher(new List<IPowerplantEnricherStrategy>
		{
			new GasFiredPowerplantEnricher(mapper),
			new TurboJetPowerplantEnricher(mapper),
			new WindTurbinePowerplantEnricher(mapper)
		})), new PowerplantPowerCalculator(new PowerplantMeritOrderSorter()));
	}

	[Fact]
    public void Should_Calculate_Power()
    {
		IEnumerable<ProductionPlanForPowerplant> calcualtedPowers = _sut.CalculateNeededPower(new Payload
		{
			Load = 480,
			Fuels = new List<Fuel>
			{
				new Fuel
				{
					FuelType = FuelType.Gas,
					Price = 13.4f
				},
				new Fuel
				{
					FuelType = FuelType.Kerosine,
					Price = 50.8f
				},
				new Fuel
				{
					FuelType = FuelType.Wind,
					Price = 60
				},
				new Fuel
				{
					FuelType = FuelType.Co2,
					Price = 20
				}
			},
			Powerplants = new List<Powerplant>
			{
				new Powerplant
				{
					Name = "gasfiredbig1",
					Efficiency = 0.53f,
					PowerMax = 460,
					PowerMin = 100,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "gasfiredbig2",
					Efficiency = 0.53f,
					PowerMax = 460,
					PowerMin = 100,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "gasfiredsomewhatsmaller",
					Efficiency = 0.37f,
					PowerMax = 210,
					PowerMin = 40,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "tj1",
					Efficiency = 0.3f,
					PowerMax = 16,
					PowerMin = 0,
					PowerplantType = PowerplantType.TurboJet
				},
				new Powerplant
				{
					Name = "windpark1",
					Efficiency = 1,
					PowerMax = 150,
					PowerMin = 0,
					PowerplantType = PowerplantType.WindTurbine
				},
				new Powerplant
				{
					Name = "windpark2",
					Efficiency = 1,
					PowerMin = 0,
					PowerMax = 36,
					PowerplantType = PowerplantType.WindTurbine
				}
			}
		});

		Assert.Equal(480, calcualtedPowers.Sum(x => x.DeliveredPower));
    }

	[Fact]
	public void Should_Adjust_Calculation()
	{
		IEnumerable<ProductionPlanForPowerplant> calcualtedPowers = _sut.CalculateNeededPower(new Payload
		{
			Load = 230,
			Fuels = new List<Fuel>
			{
				new Fuel
				{
					FuelType = FuelType.Gas,
					Price = 13.4f
				},
				new Fuel
				{
					FuelType = FuelType.Kerosine,
					Price = 50.8f
				},
				new Fuel
				{
					FuelType = FuelType.Wind,
					Price = 60f
				},
				new Fuel
				{
					FuelType = FuelType.Co2,
					Price = 20
				}
			},
			Powerplants = new List<Powerplant>
			{
				new Powerplant
				{
					Name = "gasfiredbig1",
					Efficiency = 0.53f,
					PowerMax = 460,
					PowerMin = 100,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "gasfiredbig2",
					Efficiency = 0.53f,
					PowerMax = 460,
					PowerMin = 100,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "gasfiredsomewhatsmaller",
					Efficiency = 0.37f,
					PowerMax = 210,
					PowerMin = 40,
					PowerplantType = PowerplantType.GasFired
				},
				new Powerplant
				{
					Name = "tj1",
					Efficiency = 0.3f,
					PowerMax = 16,
					PowerMin = 0,
					PowerplantType = PowerplantType.TurboJet
				},
				new Powerplant
				{
					Name = "windpark1",
					Efficiency = 1,
					PowerMax = 150,
					PowerMin = 0,
					PowerplantType = PowerplantType.WindTurbine
				},
				new Powerplant
				{
					Name = "windpark2",
					Efficiency = 1,
					PowerMin = 0,
					PowerMax = 36,
					PowerplantType = PowerplantType.WindTurbine
				}
			}

		});

		Assert.Equal(230, calcualtedPowers.Sum(x => x.DeliveredPower));
	}
}
