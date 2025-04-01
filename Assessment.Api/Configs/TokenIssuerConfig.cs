namespace Assessment.Api.Configs;

public class TokenIssuerConfig
{
    public const string SectionName = "TokenIssuerSettings";
    public string Authority { get; set; }
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string Scope { get; set; }
}