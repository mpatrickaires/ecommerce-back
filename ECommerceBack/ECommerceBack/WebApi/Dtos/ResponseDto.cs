namespace ECommerceBack.WebApi.Dtos;

public class ResponseDto<T>
{
    public ResponseDto(IEnumerable<string> mensagens, T conteudo)
    {
        Mensagens = mensagens.ToArray();
        Conteudo = conteudo;
    }

    public ResponseDto(string mensagem, T conteudo) : this(new[] { mensagem }, conteudo)
    {
    }

    public string[] Mensagens { get; }
    public T Conteudo { get; }
}

public class ResponseDto : ResponseDto<object?>
{
    public ResponseDto(IEnumerable<string> mensagens) : base(mensagens, null)
    {
    }

    public ResponseDto(string mensagem) : this(new[] { mensagem })
    {
    }
}
