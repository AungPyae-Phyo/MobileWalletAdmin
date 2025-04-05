using MobileWalletAdmin.Models.KYC;
using MobileWalletAdmin.Models.Users;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class KYCService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public KYCService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<KYCModel>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync("/kyc", cancellationToken);
                response.EnsureSuccessStatusCode();

                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");

                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;


                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new List<KYCModel>();
                }

                Console.WriteLine($"Parsed 'data' section: {dataElement}");


                try
                {
                    var users = JsonSerializer.Deserialize<List<KYCModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {users?.Count ?? 0} users.");
                    return users ?? new List<KYCModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new List<KYCModel>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new List<KYCModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: Unexpected error: {ex.Message}");
                return new List<KYCModel>();
            }
        }

        public async Task<KYCModel> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync($"/kyc/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new KYCModel();
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var user = JsonSerializer.Deserialize<KYCModel>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed user.");
                    return user ?? new KYCModel();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new KYCModel();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new KYCModel();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: Unexpected error: {ex.Message}");
                return new KYCModel();
            }
        }
    }
}
