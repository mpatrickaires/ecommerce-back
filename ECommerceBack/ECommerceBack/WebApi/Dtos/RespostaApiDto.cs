namespace ECommerceBack.WebApi.Dtos;

public class RespostaApiDto<T>
{
    public RespostaApiDto(IEnumerable<string> mensagens, T conteudo)
    {
        Mensagens = mensagens.ToArray();
        Conteudo = conteudo;
    }

    public RespostaApiDto(string mensagem, T conteudo) : this(new[] { mensagem }, conteudo)
    {
    }

    public RespostaApiDto(T conteudo) : this(Array.Empty<string>(), conteudo)
    {
    }

    public string[] Mensagens { get; }
    public T Conteudo { get; }
}

public class RespostaApiDto : RespostaApiDto<object?>
{
    public RespostaApiDto(IEnumerable<string> mensagens) : base(mensagens, null)
    {
    }

    public RespostaApiDto(string mensagem) : this(new[] { mensagem })
    {
    }
}
