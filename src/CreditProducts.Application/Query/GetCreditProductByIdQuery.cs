using CreditProducts.Application.Common;
using MediatR;

namespace CreditProducts.Application.Query;

public record GetCreditProductByIdQuery(Guid CreditProductId) : IRequest<Result<GetCreditProductByIdResponse>>;

public record GetCreditProductByIdResponse(
    Guid Id,
    string Code,
    string Name,
    decimal MinAmount,
    decimal MaxAmount,
    int MinTermMonths,
    int MaxTermMonths,
    decimal MaxInstallmentIncomeRatio,
    bool IsActive );