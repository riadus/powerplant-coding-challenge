using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using PowerplantCodingChallenge.Application.Dtos;
using PowerplantCodingChallenge.Application.Implementations;
using PowerplantCodingChallenge.Application.Validations;

[assembly: InternalsVisibleTo("PowerplantCodingChallenge.Tests")]
[assembly: InternalsVisibleTo("DynamicProxyGenAssembly2")]

namespace PowerplantCodingChallenge.Application;

public static class ConfigureServices
{
	public static IServiceCollection AddApplication(this IServiceCollection serviceCollection)
	{
		Assembly executingAssembly = Assembly.GetExecutingAssembly();
		serviceCollection.AddAutoMapper(executingAssembly);
		serviceCollection.AddScoped<IValidator<PlayloadDto>, PayloadValidation>();
		serviceCollection.AddScoped<IProductionPlanCalculator, ProductionPlanCalculator>();
		serviceCollection.AddScoped<IPowerplantFactory, PowerplantFactory>();
		serviceCollection.AddScoped<IPowerplantPowerCalculator, PowerplantPowerCalculator>(); 
		serviceCollection.AddScoped<IPowerplantEnricher, PowerplantEnricher>();
		serviceCollection.AddScoped<IPowerplantEnricherStrategy, GasFiredPowerplantEnricher>();
		serviceCollection.AddScoped<IPowerplantEnricherStrategy, WindTurbinePowerplantEnricher>();
		serviceCollection.AddScoped<IPowerplantEnricherStrategy, TurboJetPowerplantEnricher>();
		serviceCollection.AddScoped<IPowerplantMeritOrderSorter, PowerplantMeritOrderSorter>();
		return serviceCollection;
	} 
}

