using CreditProducts.Domain.Repository;
using CreditProducts.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CreditProducts.Infrastructure.Repository;

public class CreditProductRepository : ICreditProductRepository
{
    private readonly CreditProductDbContext _creditProductDbContext;

    public CreditProductRepository(CreditProductDbContext creditProductDbContext)
    {
        _creditProductDbContext = creditProductDbContext;
    }

    public async Task AddAsync(Domain.Entities.CreditProduct creditProduct, CancellationToken ct = default)
        => await _creditProductDbContext.CreditProducts.AddAsync(creditProduct, ct);

    public Task<bool> ExistsByCodeAsync(string code, CancellationToken ct = default)
        => _creditProductDbContext.CreditProducts.AnyAsync(x => x.Code == code);

    public Task<Domain.Entities.CreditProduct?> GetByIdAsync(Guid Id, CancellationToken ct = default)
        => _creditProductDbContext.CreditProducts.SingleOrDefaultAsync(x => x.Id == Id);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _creditProductDbContext.SaveChangesAsync(ct);
}