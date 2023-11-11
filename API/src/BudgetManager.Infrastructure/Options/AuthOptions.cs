namespace BudgetManager.Infrastructure.Options;

internal class AuthOptions
{
    public const string Section = "Auth";

    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpiryHours { get; set; }
    public string Key { get; set; }
    public int RefreshTokenValidInDays { get; set; }
}
