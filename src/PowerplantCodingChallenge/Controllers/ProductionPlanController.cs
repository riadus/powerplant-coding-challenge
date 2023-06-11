using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PowerplantCodingChallenge.Application;
using PowerplantCodingChallenge.Domain;
using PowerplantCodingChallenge.Application.Dtos;
using FluentValidation;
using System;
using Ardalis.GuardClauses;
using FluentValidation.Results;

namespace PowerplantCodingChallenge.Controllers;

[ApiController]
[Route("productionplan")]
public class ProductionPlanController : ControllerBase
{
    private readonly ILogger<ProductionPlanController> _logger;
	private readonly IMapper _mapper;
	private readonly IProductionPlanCalculator _productionPlanCalculator;
	private readonly IValidator<PlayloadDto> _payloadValidator;

	public ProductionPlanController(ILogger<ProductionPlanController> logger,
		IValidator<PlayloadDto> payloadValidator,
		IMapper mapper,
		IProductionPlanCalculator productionPlanCalculator)
    {
        _logger = Guard.Against.Null(logger, nameof(logger));
		_payloadValidator = Guard.Against.Null(payloadValidator, nameof(payloadValidator));
		_mapper = Guard.Against.Null(mapper, nameof(mapper));
		_productionPlanCalculator = Guard.Against.Null(productionPlanCalculator, nameof(productionPlanCalculator));
	}

    [HttpPost]
    public IActionResult CalculateProductionPlan(PlayloadDto payloadDto)
    {
		try
		{
			ValidationResult validation = _payloadValidator.Validate(payloadDto);
			if(!validation.IsValid)
			{
				_logger.LogWarning($"Validation of {nameof(payloadDto)} failed");
				return BadRequest(validation.Errors);
			}
			IEnumerable<ProductionPlanForPowerplant> result = _productionPlanCalculator.CalculateNeededPower(_mapper.Map<Payload>(payloadDto));
			return Ok(result.Select(_mapper.Map<PowerplantPowerDto>));
		}
		catch(Exception ex)
		{
			_logger.LogError("An error occured", ex.Message);
			throw;
		}
	}
}