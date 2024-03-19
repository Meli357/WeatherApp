namespace WeatherApp.Dto;

public class ForecastDto
{
    public ForecastCurrentUnitsDto CurrentUnits { get; set; } = new();
    public ForecastCurrentDto Current { get; set; } = new();
}