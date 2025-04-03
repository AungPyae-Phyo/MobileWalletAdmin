using MobileWalletAdmin.Models.Users;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class UserService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public UserService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<UserModel>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync("/user/with-wallet-kyc", cancellationToken);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;

             
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new List<UserModel>();
                }

                Console.WriteLine($"Parsed 'data' section: {dataElement}");

       
                try
                {
                    var users = JsonSerializer.Deserialize<List<UserModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {users?.Count ?? 0} users.");
                    return users ?? new List<UserModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new List<UserModel>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new List<UserModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: Unexpected error: {ex.Message}");
                return new List<UserModel>();
            }
        }
    }
}
