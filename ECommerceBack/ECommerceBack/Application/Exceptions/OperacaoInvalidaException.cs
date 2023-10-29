namespace ECommerceBack.Application.Exceptions;

public class OperacaoInvalidaException : Exception
{
    public OperacaoInvalidaException(string? message) : base(message)
    {
    }
}
