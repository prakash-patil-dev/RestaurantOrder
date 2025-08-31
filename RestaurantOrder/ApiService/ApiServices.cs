using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;

namespace RestaurantOrder.ApiService;

public static class ApiClient
{
    private static readonly HttpClient _httpClient;
    private static string _baseUrl;

    static ApiClient()
    {
#if ANDROID
        _baseUrl = "http://10.252.56.220:4545";
#else
        _baseUrl = "http://localhost:4545";
#endif

        _httpClient = new HttpClient(new SocketsHttpHandler
        {
            PooledConnectionLifetime = TimeSpan.FromMinutes(2),
            MaxConnectionsPerServer = 10,
            AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
        })
        {
            Timeout = TimeSpan.FromSeconds(20)
        };

        _httpClient.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
    }

    public static void SetBaseUrl(string url) => _baseUrl = url;

    private static async Task<bool> HandleErrorAsync(HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            string errorContent = await response.Content.ReadAsStringAsync();
            Debug.WriteLine($"HTTP {(int)response.StatusCode} {response.ReasonPhrase}: {errorContent}");
            return false;
        }
        return true;
    }

    private static async Task<T?> ExecuteWithRetryAsync<T>(Func<Task<T?>> action, int retries = 3, int delayMs = 300)
    {
        for (int attempt = 1; attempt <= retries; attempt++)
        {
            try
            {
                return await action();
            }
            catch (HttpRequestException ex) when (attempt < retries)
            {
                Debug.WriteLine($"[Retry {attempt}] Network error: {ex.Message}");
            }
            catch (TaskCanceledException ex) when (attempt < retries)
            {
                Debug.WriteLine($"[Retry {attempt}] Timeout: {ex.Message}");
            }
            catch (Exception ex) // final fallback
            {
                Debug.WriteLine($"[Attempt {attempt}] Unexpected error: {ex}");
                if (attempt >= retries) throw; // last attempt -> bubble up if needed
            }

            await Task.Delay(delayMs * attempt);
        }
        return default;
    }

    public static Task<T?> GetAsync<T>(string endpoint) =>
        ExecuteWithRetryAsync(async () =>
        {
            try
            {
                var fullUri = $"{_baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
                if (!Uri.TryCreate(fullUri, UriKind.Absolute, out var uri))
                    return default;


                var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}", HttpCompletionOption.ResponseHeadersRead)
                                                .ConfigureAwait(false);

                if (!await HandleErrorAsync(response)) return default;
                return await response.Content.ReadFromJsonAsync<T>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"GET {endpoint} failed: {ex}");
                throw;
            }
        });

    public static Task<TResponse?> PostAsync<TRequest, TResponse>(string endpoint, TRequest data) =>
        ExecuteWithRetryAsync(async () =>
        {
            try
            {
                var fullUri = $"{_baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
                if (!Uri.TryCreate(fullUri, UriKind.Absolute, out var uri))
                    return default; //throw new ArgumentException($"Invalid URI: {fullUri}");


                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{endpoint}", data).ConfigureAwait(false);
                if (!await HandleErrorAsync(response)) return default;
                return await response.Content.ReadFromJsonAsync<TResponse>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"POST {endpoint} failed: {ex}");
                throw;
            }
        });

    public static Task<bool> PostNoResponseAsync<TRequest>(string endpoint, TRequest data) =>
        ExecuteWithRetryAsync(async () =>
        {
            try
            {
                var fullUri = $"{_baseUrl.TrimEnd('/')}/{endpoint.TrimStart('/')}";
                if (!Uri.TryCreate(fullUri, UriKind.Absolute, out var uri))
                    return default;


                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/{endpoint}", data).ConfigureAwait(false);
                return await HandleErrorAsync(response);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"POST (no response) {endpoint} failed: {ex}");
                throw;
            }
        });
}
