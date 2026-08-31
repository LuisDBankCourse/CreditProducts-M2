namespace CreditProducts.Domain.Entities;

public class CreditProduct
{
    public Guid Id { get; set; }
    public string Code { get; set; }
    public string Name { get; set; }
    public decimal MinAmount { get; set; }
    public decimal MaxAmount { get; set; }
    public int MinTermMonths { get; set; }
    public int MaxTermMonths { get; set; }
    public decimal MaxInstallmentIncomeRatio { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAtUtc { get; set; }
    
    public static CreditProduct Create(
        string code, 
        string name, 
        decimal minAmount, 
        decimal maxAmount, 
        int minTermMonths,
        int maxTermMonths,
        decimal maxInstallmentIncomeRatio,
        bool isActive = true)
    {

        ArgumentException.ThrowIfNullOrWhiteSpace(code);
        ArgumentException.ThrowIfNullOrWhiteSpace(name);
            
        if (maxAmount <= minAmount)
            throw new ArgumentOutOfRangeException((nameof(maxAmount)), "maxAmount must be greater than minAmount");
        
        if (minTermMonths <= 0)
            throw new ArgumentOutOfRangeException((nameof(minTermMonths)), "minTermMonths must be greater than 0");
        
        if (maxTermMonths <= minTermMonths)
            throw new ArgumentOutOfRangeException((nameof(maxTermMonths)), "maxTermMonths must be greater than minTermMonths");
        
        if (maxInstallmentIncomeRatio is <= 0 or >= 1)
            throw new ArgumentOutOfRangeException((nameof(maxInstallmentIncomeRatio)), "maxInstallmentIncomeRatio must be greater than 0 and less or equal 1");


        return new CreditProduct
        {
            Id = Guid.NewGuid(),
            Code = code.Trim(),
            Name = name.Trim(),
            MinAmount = minAmount,
            MaxAmount = maxAmount,
            MinTermMonths = minTermMonths,
            MaxTermMonths = maxTermMonths,
            MaxInstallmentIncomeRatio = maxInstallmentIncomeRatio,
            IsActive = isActive,
            CreatedAtUtc = DateTime.UtcNow
        };
    }
}