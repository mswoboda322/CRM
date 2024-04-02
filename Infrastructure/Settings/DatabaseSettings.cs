namespace Infrastructure.Settings;

public class DatabaseSettings
{
    public string? ConnectionString { get; set; }
    public bool AutoMigrate { get; set; }
}
