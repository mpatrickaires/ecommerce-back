namespace ECommerceBack.Domain.Validators;

public static class MensagemValidacao
{
    public static string CampoObrigatorio => "{PropertyName} é um campo obrigatório.";
    public static string MenorQue => "{PropertyName} deve possuir um valor menor que {ComparisonValue}.";
    public static string EscalaPrecisao => "{PropertyName} pode possuir até {ExpectedPrecision} dígitos, sendo {ExpectedScale} casas decimais.";
}
