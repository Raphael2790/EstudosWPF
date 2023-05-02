using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using WeatherApp.Model;

namespace WeatherApp.ViewModel.Helpers;

public static class AccuWeatherHelper
{
    private const string BASE_URL = "http://dataservice.accuweather.com/";
    private const string AUTOCOMPLETE_ENDPOINT = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
    private const string CURRENT_CONDITIONS_ENDPOINT = "currentconditions/v1/{0}?apikey={1}";
    private const string API_KEY = "d3Mk4prAROPZiZPiyr49i4nD4PxryFAd";

    public static async Task<List<City>?> GetCities(string query)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(BASE_URL + string.Format(AUTOCOMPLETE_ENDPOINT, API_KEY, query));
        var result = await response.Content.ReadAsStringAsync();
        var cities = JsonSerializer.Deserialize<List<City>>(result);
        return cities;
    }

    public static async Task<CurrentConditions?> GetCurrentConditions(string cityKey)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(BASE_URL + string.Format(CURRENT_CONDITIONS_ENDPOINT, cityKey, API_KEY));
        var result = await response.Content.ReadAsStringAsync();
        var currentConditions = JsonSerializer.Deserialize<List<CurrentConditions>>(result);
        return currentConditions?.FirstOrDefault();
    }
}
