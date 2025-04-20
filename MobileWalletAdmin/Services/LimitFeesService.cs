using MobileWalletAdmin.Models.LimitFees;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class LimitFeesService
    {
        public readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;

        public LimitFeesService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<LimitFeesModel>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync("/limitfees/all", cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new List<LimitFeesModel>();
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var limitFees = JsonSerializer.Deserialize<List<LimitFeesModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {limitFees?.Count ?? 0} limit fees.");
                    return limitFees ?? new List<LimitFeesModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new List<LimitFeesModel>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new List<LimitFeesModel>();
            }
        }


        public async Task<List<LimitFeesModel>> GetById(string id, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync($"/limifees/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new List<LimitFeesModel>();
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var limitFees = JsonSerializer.Deserialize<List<LimitFeesModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {limitFees?.Count ?? 0} limit fees.");
                    return limitFees ?? new List<LimitFeesModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new List<LimitFeesModel>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new List<LimitFeesModel>();
            }
        }

        public async Task<LimitFeesModel?> Create(LimitFeesModel limitFees, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonSerializer.Serialize(limitFees, _options);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _http.PostAsync("/limitfees", content, cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return null;
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var createdLimitFees = JsonSerializer.Deserialize<LimitFeesModel>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully created limit fees.");
                    return createdLimitFees;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return null;
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return null;
            }
        }   


        public async Task<bool> Update(LimitFeesModel limitFees, CancellationToken cancellationToken)
        {
            try
            {
                var json = JsonSerializer.Serialize(limitFees, _options);
                var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _http.PutAsync($"/limitfees/{limitFees.limit_fees_id}", content, cancellationToken);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return false;
            }
        }

        public async Task<bool> Delete(string id, CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.DeleteAsync($"/limitfees/{id}", cancellationToken);
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return false;
            }
        }
    }
}