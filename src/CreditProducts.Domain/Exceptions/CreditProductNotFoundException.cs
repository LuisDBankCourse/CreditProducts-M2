namespace CreditProducts.Domain.Exceptions;

public class CreditProductNotFoundException : Exception
{
    public CreditProductNotFoundException(Guid creditProductId)
        :base($"The product with the id {creditProductId} does not exist") { }
}

public class DuplicateProductCodeException : Exception
{
    public DuplicateProductCodeException(string code)
        : base($"The product code: {code} already exists") { }
}