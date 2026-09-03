using CreditProducts.Application.Common;
using CreditProducts.Domain.Repository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CreditProducts.Application.Query;

public class GetCreditProductByIdHandler :  IRequestHandler<GetCreditProductByIdQuery, Result<GetCreditProductByIdResponse>>
{
    private readonly ICreditProductRepository _creditProductRepository;
    private readonly ILogger _logger;

    public GetCreditProductByIdHandler(ICreditProductRepository creditProductRepository, ILogger<GetCreditProductByIdHandler> logger)
    {
        _creditProductRepository = creditProductRepository;
        _logger = logger;
    }

    public async Task<Result<GetCreditProductByIdResponse>> Handle(GetCreditProductByIdQuery request, CancellationToken ct)
    {
        var creditProduct = await _creditProductRepository.GetByIdAsync(request.CreditProductId);

        if (creditProduct is null)
        {
            _logger.LogWarning("Credit product with CreditProductId={CreditProductId} not found",
                request.CreditProductId);
            return Result<GetCreditProductByIdResponse>.Failure($"Credit product with CreditProductId={request.CreditProductId} doesn't exists");
        }

        return Result<GetCreditProductByIdResponse>.Success(new GetCreditProductByIdResponse(
            creditProduct.Id,
            creditProduct.Code,
            creditProduct.Name,
            creditProduct.MinAmount,
            creditProduct.MaxAmount,
            creditProduct.MinTermMonths,
            creditProduct.MaxTermMonths,
            creditProduct.MaxInstallmentIncomeRatio,
            creditProduct.IsActive
        ));
    }
}