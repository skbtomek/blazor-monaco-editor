namespace BlazorMonacoEditor.TypeDefinitions;

public class WeatherReport
{
    public string Title { get; set; } = null!;
    public DateTime PublicationDate { get; set; }
    public ReportType Type { get; set; }
    public Measurements Measurements { get; set; } = null!;
}

public enum ReportType
{
    Hourly,
    Daily,
    Weekly,
    Monthly
}

public class Measurements {
    public int? TemperatureC { get; set; }
    public int? TemperatureF { get; set; }
    public WindSpeed WindSpeed { get; set; } = null!;
}

public class WindSpeed
{
    public double Measure { get; set; }
    public Speed Unit { get; set; }
}

public enum Speed
{
    KmH,
    Ms
}

