using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Client;

internal class KaspiClient
{
    private readonly HttpClient _httpClient;

    [ActivatorUtilitiesConstructor]
    public KaspiClient(IOptions<ClientOptions> options, HttpClient httpClient) : this(options.Value, httpClient)
    {
    }

    public KaspiClient(ClientOptions options, HttpClient? httpClient = null)
    {
        _httpClient = httpClient ?? new HttpClient();
        _httpClient.BaseAddress = new Uri(string.IsNullOrWhiteSpace(options.ApiBaseAddress) ? "https://kaspi.kz/shop/" : options.ApiBaseAddress);
        _httpClient.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

        if (string.IsNullOrWhiteSpace(options.ApiKey))
        {
            throw new ArgumentException(nameof(options.ApiKey));
        }
    }

    //TODO below sample methods and you can delete them safely
    public async Task<string> GetAsync(string productCode)
    {
        var url = $"{_httpClient.BaseAddress}rest/misc/product/mobile/specifications?productCode={productCode}";

        var response = await _httpClient.GetAsync(url).ConfigureAwait(false);

        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        

        return responseBody;
    }

}
