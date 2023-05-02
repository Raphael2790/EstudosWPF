using EvernoteClone.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace EvernoteClone.Helpers;

public static class FirebaseAuthHelper
{
    private readonly static string _apiKey = "API_KEY";

    public static async Task<bool> Register(User user)
    {
        using HttpClient httpClient = new HttpClient();

        var body = new
        {
            email = user.Username,
            password = user.Password,
            returnSecureToken = true
        };

        var json = JsonSerializer.Serialize(body);

        var response = await httpClient.PostAsync(
                       $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_apiKey}",
                                  new StringContent(json, Encoding.UTF8, "application/json"));

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<RegisterRespose>(content);
            App.UserId = result!.localId;
            return true;
        }
        else
        {
            var error = JsonSerializer.Deserialize<ErrorResponse>(content);
            MessageBox.Show(error!.error.message);
            return false;
        }
    }

    public static async Task<bool> Login(User user)
    {
        using HttpClient httpClient = new HttpClient();

        var body = new
        {
            email = user.Username,
            password = user.Password,
            returnSecureToken = true
        };

        var json = JsonSerializer.Serialize(body);

        var response = await httpClient.PostAsync(
                       $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_apiKey}",
                                  new StringContent(json, Encoding.UTF8, "application/json"));

        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            var result = JsonSerializer.Deserialize<RegisterRespose>(content);
            App.UserId = result!.localId;
            return true;
        }
        else
        {
            var error = JsonSerializer.Deserialize<ErrorResponse>(content);
            MessageBox.Show(error!.error.message);
            return false;
        }
    }
}

public class RegisterRespose
{
    public string kind { get; set; }
    public string idToken { get; set; }
    public string email { get; set; }
    public string refreshToken { get; set; }
    public string expiresIn { get; set; }
    public string localId { get; set; }
}

public class ErrorDetails
{
    public string code { get; set; }
    public string message { get; set; }
}

public class ErrorResponse
{
    public ErrorDetails error { get; set; }
}
