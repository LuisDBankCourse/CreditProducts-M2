namespace CreditProducts.Domain.Repository;

public interface ICreditProductRepository
{
    Task<Entities.CreditProduct?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default);
    Task AddAsync(Entities.CreditProduct creditProduct, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}