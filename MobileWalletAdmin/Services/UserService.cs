using MobileWalletAdmin.Models.Users;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class UserService
    {
        private readonly HttpClient _http;

        public UserService(HttpClient http)
        {
            _http = http;
        }
        public async Task<List<UserModel>> GetAll(CancellationToken cancellationToken)
        {
            HttpResponseMessage response = await _http.GetAsync("/user/all-with-wallet-kyc", cancellationToken);
            response.EnsureSuccessStatusCode();

            string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);

            using JsonDocument doc = JsonDocument.Parse(jsonResponse);
            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("data", out JsonElement dataElement))
            {
                return JsonSerializer.Deserialize<List<UserModel>>(dataElement.GetRawText(), new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }) ?? new List<UserModel>();
            }

            return new List<UserModel>();
        }

    }
}

