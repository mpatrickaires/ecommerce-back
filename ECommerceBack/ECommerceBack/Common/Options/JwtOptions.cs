namespace ECommerceBack.Common.Options;

public class JwtOptions
{
    public const string Position = "JwtOptions";

    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}
