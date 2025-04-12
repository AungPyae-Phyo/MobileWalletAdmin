using MobileWalletAdmin.Models.Wallet;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class WalletService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;
        public WalletService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public Task<List<WalletModel>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = _http.GetAsync("/wallet/all", cancellationToken).Result;
                response.EnsureSuccessStatusCode();
                string jsonResponse = response.Content.ReadAsStringAsync(cancellationToken).Result;
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return Task.FromResult(new List<WalletModel>());
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var wallets = JsonSerializer.Deserialize<List<WalletModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {wallets?.Count ?? 0} wallets.");
                    return Task.FromResult(wallets ?? new List<WalletModel>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return Task.FromResult(new List<WalletModel>());
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return Task.FromResult(new List<WalletModel>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: An unexpected error occurred: {ex.Message}");
                return Task.FromResult(new List<WalletModel>());
            }
        }

        public async Task<WalletModel?> UpdateBalance(WalletModel model, CancellationToken cancellationToken)
        {
            try
            {
               
                var content = new StringContent(JsonSerializer.Serialize(new
                {
                    walletId = model.Id,
                    balance = model.Balance
                }), System.Text.Encoding.UTF8, "application/json");

                var response = await _http.PutAsync("/wallet/update-balance", content, cancellationToken);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);

                if (!doc.RootElement.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine("ERROR: 'data' section missing in update response");
                    return null;
                }

                return JsonSerializer.Deserialize<WalletModel>(dataElement.GetRawText(), _options);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update failed: {ex.Message}");
                return null;
            }
        }


    }
}
