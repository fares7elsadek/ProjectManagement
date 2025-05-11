namespace ProjectManagement.Domain.Helpers;

public class JwtOptions
{
    public string Issure { get; set; } = default!;
    public string Audience { get; set; } = default!;
    public string Key { get; set; } = default!;
    public Double DurationInMinutes { get; set; } = default!;
}