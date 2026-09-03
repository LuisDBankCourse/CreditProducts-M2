using CreditProducts.Application.Commands;
using CreditProducts.Application.Query;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CreditProducts.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CreditProductsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IValidator<CreateCreditProductCommand> _validator;
    private readonly ILogger<CreditProductsController> _logger;

    public CreditProductsController(IMediator mediator, IValidator<CreateCreditProductCommand> validator,
        ILogger<CreditProductsController> logger)
    {
        _mediator = mediator;
        _validator = validator;
        _logger = logger;
    }

    [HttpPost]
    [ProducesResponseType(typeof(CreateCreditProductResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(CreateCreditProductResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(CreateCreditProductResponse), StatusCodes.Status409Conflict)]
    public async Task<IActionResult> CreateCustomer([FromBody] CreateCreditProductCommand command, CancellationToken ct)
    {
        var validation = await _validator.ValidateAsync(command, ct);

        if (!validation.IsValid)
        {
            var errors = validation.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(g => g.Key, g => g
                    .Select(x => x.ErrorMessage)
                    .ToArray());

            return ValidationProblem(new ValidationProblemDetails(errors)
            {
                Title = "Validation Error",
                Status = StatusCodes.Status400BadRequest,
            });
        }

        var result = await _mediator.Send(command, ct);

        if (!result.IsSuccess)
        {
            return Problem(
                title: "Conflict",
                detail: result.Error,
                statusCode: StatusCodes.Status409Conflict

            );
        }

        return CreatedAtAction(nameof(GetByIdCustomer),
           new{id = result.Value}, result.Value );

    }
    
    [HttpGet("{Id}")]
    
    public async Task<ActionResult> GetByIdCustomer(Guid id, CancellationToken ct)
    {
        var result = await _mediator.Send(new GetCreditProductByIdQuery(id), ct);

        if (!result.IsSuccess)
        {
            return Problem(
                title: "NotFound",
                detail: result.Error,
                statusCode: StatusCodes.Status404NotFound);
        }
        return Ok(result.Value);
    }
    
}