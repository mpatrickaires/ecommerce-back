namespace ECommerceBack.Infra.Options;

public class ECommerceContextOptions
{
    public const string Position = "ECommerceContextOptions";

    public string Host { get; set; }
    public string Database { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
}
