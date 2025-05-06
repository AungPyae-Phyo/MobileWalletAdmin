
using MobileWalletAdmin.Models.TransactionHistory;
using System.Text.Json;

namespace MobileWalletAdmin.Services
{
    public class TransactionHistoryService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _options;
        public TransactionHistoryService(HttpClient http)
        {
            _http = http;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<List<TransactionHistoryModel>> GetAll(CancellationToken cancellationToken)
        {
            try
            {
                HttpResponseMessage response = await _http.GetAsync("/api/transaction/history", cancellationToken);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync(cancellationToken);
                Console.WriteLine($"Raw JSON Response: {jsonResponse}");
                using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                JsonElement root = doc.RootElement;
                if (!root.TryGetProperty("data", out JsonElement dataElement))
                {
                    Console.WriteLine(" ERROR: 'data' section missing in API response!");
                    return new List<TransactionHistoryModel>();
                }
                Console.WriteLine($"Parsed 'data' section: {dataElement}");
                try
                {
                    var transactionHistories = JsonSerializer.Deserialize<List<TransactionHistoryModel>>(dataElement.GetRawText(), _options);
                    Console.WriteLine($"Successfully parsed {transactionHistories?.Count ?? 0} transaction histories.");
                    return transactionHistories ?? new List<TransactionHistoryModel>();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($" ERROR: Failed to deserialize JSON: {ex.Message}");
                    return new List<TransactionHistoryModel>();
                }
            }
            catch (HttpRequestException httpEx)
            {
                Console.WriteLine($" ERROR: HTTP Request failed: {httpEx.Message}");
                return new List<TransactionHistoryModel>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" ERROR: An unexpected error occurred: {ex.Message}");
                return new List<TransactionHistoryModel>();
            }
        }
    }
}
