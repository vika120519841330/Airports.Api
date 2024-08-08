using Airports.BusinessLayer.Helpers;
using Airports.Shared.Models;
using Airports.Shared.Static;
using Microsoft.Extensions.Options;
using System.Net.Http;

namespace Airports.BusinessLayer.Services;

public class HttpService
{
    private readonly IHttpClientFactory httpClientFactory;
    private readonly ConfigBase configs;

    public HttpService(IHttpClientFactory httpClientFactory, IOptions<ConfigBase> options)
    {
        this.httpClientFactory = httpClientFactory;
        this.configs = options.Value;
    }

    public string NotifyMessage { get; private set; } = string.Empty;

    private HttpClient HttpClient => httpClientFactory.CreateClient(StaticItems.airportsHttpClient);

    public async Task<TResponse> SendRequestAsync<TResponse>(
        string url,
        HttpMethod httpMethod,
        CancellationToken token = default)
    {
        NotifyMessage = string.Empty;
        var request = url.GetRequestMessage(httpMethod);
        var response = await HttpClient.SendRequest(request, token);

        if (response == null)
        {
            NotifyMessage = response?.ReasonPhrase ?? "Операция закончилась с ошибкой !";
            return default;
        }

        return await response.DeserializeTResponse<TResponse>();
    }
}