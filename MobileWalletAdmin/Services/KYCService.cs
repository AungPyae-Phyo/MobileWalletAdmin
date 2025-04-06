using MobileWalletAdmin.Models.KYC;
using MobileWalletAdmin.Models.Users;
using System.Net;
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
        public async Task<bool> UpdateStatus(string id, KYCModel kycModel, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _http.PutAsJsonAsync($"/kyc/update-status/{id}", kycModel, cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    return true;
                }

                // Handle specific status codes
                if (response.StatusCode == HttpStatusCode.BadRequest)
                {
                    var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
                    Console.WriteLine($"BadRequest details: {errorContent}");

                    // You could parse the error response if needed
                    try
                    {
                        var errorResponse = JsonSerializer.Deserialize<ErrorResponse>(errorContent, _options);
                        Console.WriteLine($"Error message: {errorResponse?.Message}");
                        foreach (var error in errorResponse?.Errors ?? new List<ErrorDetail>())
                        {
                            Console.WriteLine($"Field: {error.Field}, Message: {error.Message}");
                        }
                    }
                    catch { }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating status: {ex.Message}");
                return false;
            }
        }

        // Add these classes to handle error responses
        public class ErrorResponse
        {
            public string Message { get; set; }
            public string Status { get; set; }
            public List<ErrorDetail> Errors { get; set; }
        }

        public class ErrorDetail
        {
            public string Field { get; set; }
            public string Message { get; set; }
        }
    }
}
