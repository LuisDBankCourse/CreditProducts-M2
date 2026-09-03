using CreditProducts.Application.Common;
using MediatR;

namespace CreditProducts.Application.Commands;

public record CreateCreditProductCommand(
     string Code,
     string Name,
     decimal MinAmount,
     decimal MaxAmount,
     int MinTermMonths,
     int MaxTermMonths,
     decimal MaxInstallmentIncomeRatio,
     bool IsActive 
): IRequest<Result<CreateCreditProductResponse>>;


public record CreateCreditProductResponse(
    Guid CreditProductId,
    string Message

);