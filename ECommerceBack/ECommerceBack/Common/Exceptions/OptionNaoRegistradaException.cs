namespace ECommerceBack.Common.Exceptions;

public class OptionNaoRegistradaException : Exception
{
    public OptionNaoRegistradaException(string position) 
        : base($"Option {position} não encontrada. É preciso registra-la no appsettings.")
    {
    }
}
