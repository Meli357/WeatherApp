//https://api.open-meteo.com/v1/forecast?latitude=43.769562&longitude=11.255814&current=temperature
//https://geocoding-api.open-meteo.com/v1/search?name=berlin&count=1

using System.Net.Http.Json;
using WeatherApp.Dto;
using System.Text.Json;
using System.Globalization;

CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

const string
    Celsius = "celsius",
    Fahrenheit = "fahrenheit";

using var client = new HttpClient();

var snakeCaseOptions = new JsonSerializerOptions
{
    PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
};

string location = "Florence";
string unit = Celsius;

while (true)
{
    Console.WriteLine("Weather app");
    Console.WriteLine($"Location: {location}");
    Console.WriteLine();

    var result = await GetForecastAsync(43.769562M, 11.255814M, unit);

    Console.WriteLine($"{result!.Current.Temperature}{result!.CurrentUnits.Temperature}");

    while (!ReadCommand());

    Thread.Sleep(2000);
    Console.Clear();
}

bool ReadCommand()
{
    switch (Console.ReadKey(true).Key)
    {
        case ConsoleKey.Escape or ConsoleKey.Q:
            Environment.Exit(0);
            return false;

        case ConsoleKey.R:
            Console.WriteLine("Refreshing...");
            return true;

        case ConsoleKey.C:
            if (unit == Celsius)
                return false;

            unit = Celsius;
            Console.WriteLine("Updating unit...");
            return true;

        case ConsoleKey.F:
            if (unit == Fahrenheit)
                return false;

            unit = Fahrenheit;
            Console.WriteLine("Updating unit...");
            return true;

        default:
            return false;
    }
}

Task<GeocodingDto?> GetGeocodingDtoAsync(string location) =>
    client.GetFromJsonAsync<GeocodingDto>
    (
        $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature&temperature_unit={unit}",
        snakeCaseOptions
    );

Task<ForecastDto?> GetForecastDtoAsync(decimal latitude, decimal longitude, string unit) =>
    client.GetFromJsonAsync<ForecastDto>
    (
        $"https://api.open-meteo.com/v1/forecast?latitude={latitude}&longitude={longitude}&current=temperature&temperature_unit={unit}",
        snakeCaseOptions
    );