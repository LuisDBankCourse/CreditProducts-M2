using FluentValidation;

namespace CreditProducts.Application.Commands;

public class CreateCreditProductValidator : AbstractValidator<CreateCreditProductCommand>
{
    public CreateCreditProductValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty().WithMessage("Code is required")
            .MaximumLength(50).WithMessage("Code must not exceed 50 characters");
        
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required")
            .MaximumLength(200).WithMessage("Name must not exceed 200 characters");
        
        RuleFor(x => x.MinAmount)
            .GreaterThan(0).WithMessage("MinAmount must be greater than 0");
        
        RuleFor(x => x.MaxAmount)
            .GreaterThan(0).WithMessage("MaxAmount must be greater than 0")
            .GreaterThan(x => x.MinAmount)
            .WithMessage("MaxAmount must be greater than MinAmount");
        
        RuleFor(x =>x.MinTermMonths)
            .GreaterThan(0).WithMessage("MinTermMonths must be greater than 0");
        
        RuleFor(x => x.MaxTermMonths)
            .GreaterThan(0).WithMessage("MaxTermMonths must be greater than 0")
            .GreaterThan(x => x.MinTermMonths)
            .WithMessage("MaxTermMonths must be greater than MinTermMonths");
        
        RuleFor(x => x.MaxInstallmentIncomeRatio)
            .GreaterThan(0).WithMessage("MaxInstallmentIncomeRatio must be greater than 0")
            .LessThan(1).WithMessage("MaxInstallmentIncomeRatio must be less than 1");
    }
}