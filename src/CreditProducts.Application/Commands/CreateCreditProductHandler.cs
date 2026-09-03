using CreditProducts.Application.Common;
using CreditProducts.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CreditProducts.Application.Commands;

public class CreateCreditProductHandler : IRequestHandler<CreateCreditProductCommand, Result<CreateCreditProductResponse>>
{
    private readonly ICreditProductRepository _creditProductRepository;
    private readonly ILogger<CreateCreditProductHandler> _logger;

    public CreateCreditProductHandler(ICreditProductRepository creditProductRepository, ILogger<CreateCreditProductHandler> logger)
    {
        _creditProductRepository = creditProductRepository;
        _logger = logger;
    }

    public async Task<Result<CreateCreditProductResponse>> Handle(CreateCreditProductCommand request, CancellationToken ct)
    {
        _logger.LogInformation("Starting to create credit product code={Code} name={Name}",
            request.Code, request.Name);

        var exists = await _creditProductRepository.ExistsByCodeAsync(request.Code);

        if (exists)
        {
            _logger.LogWarning("creditProduct with Code={Code} already exists",
                request.Code);
            return Result<CreateCreditProductResponse>.Failure(
                $"credit product with the Code={request.Code} already exists");
        }

        var creditProduct = Domain.Entities.CreditProduct.Create(
            request.Code,
            request.Name,
            request.MinAmount,
            request.MaxAmount,
            request.MinTermMonths,
            request.MaxTermMonths,
            request.MaxInstallmentIncomeRatio,
            request.IsActive
        );

        await _creditProductRepository.AddAsync(creditProduct, ct);
        await _creditProductRepository.SaveChangesAsync(ct);

        _logger.LogInformation("credit product with the Code={Code} and Id={Id} Created",
            creditProduct.Code, creditProduct.Id);

        return Result<CreateCreditProductResponse>.Success(new CreateCreditProductResponse(creditProduct.Id,
            "credit product Created successfully"));
    }
}