using Serilog;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Runtime;
using System.Text.Json.Serialization;
using System.Text.Json;
using Newtonsoft.Json;
using NewtonSerializer = Newtonsoft.Json.JsonSerializer;
using SystemSerializer = System.Text.Json.JsonSerializer;

namespace Airports.BusinessLayer.Helpers;

public static class HttpClientHelper
{
    private static JsonSerializerOptions SerializersOptions
    => new JsonSerializerOptions
    {
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        PropertyNameCaseInsensitive = false,
        IncludeFields = false,
        IgnoreReadOnlyFields = true,
        IgnoreReadOnlyProperties = false,
        MaxDepth = 5,
        DefaultBufferSize = 32 * 1024 * 1024, // 16Kb default - up to 32Mb
        UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement,
    };

    private const string contentType = "application/json";

    public static HttpRequestMessage GetRequestMessage(this string uri, HttpMethod httpMethod)
        => new HttpRequestMessage(httpMethod, uri);

    public static async Task<HttpResponseMessage> SendRequest(this HttpClient httpClient, HttpRequestMessage request, CancellationToken token = default)
    {
        var details = $"при отправке http-запроса по адресу «{request?.RequestUri?.ToString() ?? string.Empty}» ";
        httpClient.DefaultRequestHeaders.CacheControl = CacheControlHeaderValue.Parse("no-cache");

        try
        {
            var response = await httpClient.SendAsync(
                request: request,
                completionOption: HttpCompletionOption.ResponseContentRead,
                cancellationToken: token).ConfigureAwait(false);

            if (response == null)
            {
                Log.Error($"Пустой ответ. Ошибка на уровне сервера {details}. ");
            }

            if (!response.IsSuccessStatusCode)
            {
                details += $"{nameof(response.StatusCode)}: {response.StatusCode}, {nameof(response.ReasonPhrase)}: {response.ReasonPhrase}";
                Log.Error($"Ошибка на уровне сервера {details}. ");
                return default;
            }

            return response;
        }
        catch (Exception exc)
        {
            Log.Error($"Исключительная ситуация {details}. " +
                      $"Подробности: {exc.Message ?? exc.InnerException?.Message ?? string.Empty}");
            return default;
        }
    }

    public static async Task<TResponse> DeserializeTResponse<TResponse>(this HttpResponseMessage response)
    {
        var details = $"Http-request to uri {response?.RequestMessage?.RequestUri?.ToString() ?? string.Empty}. ";

        if ((response?.Content?.Headers?.ContentLength ?? 0) == 0)
        {
            Log.Error($"Empty content. {details}. ");
            return default;
        }

        try
        {
            using var data = await response?.Content.ReadAsStreamAsync();
            if ((data?.Length ?? 0) == 0)
            {
                Log.Error($"Content don't reed. {details}");
                return default;
            }

            using var streamReader = new StreamReader(data);
            using var reader = new JsonTextReader(streamReader);
            reader.SupportMultipleContent = true;
            var serializer = new NewtonSerializer();
            return serializer.Deserialize<TResponse>(reader);
        }
        catch (OutOfMemoryException exc)
        {
            GCSettings.LargeObjectHeapCompactionMode = System.Runtime.GCLargeObjectHeapCompactionMode.CompactOnce;
            GC.Collect();
            Log.Information($"Start GC - {exc.Message ?? exc.InnerException?.Message ?? string.Empty}");

            // Запуск считывания после сборки мусора на всех поколениях кучи
            var responseBody = await response?.Content?.ReadAsByteArrayAsync();
            if ((responseBody?.Length ?? 0) == 0)
            {
                Log.Error($"Content don't reed. {details}");
                throw;
            }

            return responseBody != default ? SystemSerializer.Deserialize<TResponse>(responseBody, SerializersOptions) : default;
        }
        catch (Exception exc)
        {
            Log.Error($"Deserialize error (content lenth: {response?.Content?.Headers?.ContentLength ?? 0} byte). {details}" +
                      $"Error details: {exc.Message ?? exc.InnerException?.Message ?? string.Empty} ");
            throw;
        }
    }
}
