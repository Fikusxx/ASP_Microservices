using Newtonsoft.Json;
using System.Text;

namespace Shop.Web;

public class BaseService : IBaseService
{
    public ResponseDTO ResponseDTO { get; set; }
    public IHttpClientFactory HttpClientFactory { get; set; }

    public BaseService(IHttpClientFactory HttpClientFactory)
    {
        ResponseDTO = new ResponseDTO();
        this.HttpClientFactory = HttpClientFactory;
    }

    public async Task<T> SendAsync<T>(ApiRequest apiRequest)
    {
        try
        {
            var client = HttpClientFactory.CreateClient("Shop");
            var message = new HttpRequestMessage();
            message.Headers.Add("Accept", "application/json");
            message.RequestUri = new Uri(apiRequest.Url);
            client.DefaultRequestHeaders.Clear();

            if (apiRequest.Data != null)
            {
                message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                    Encoding.UTF8, "application/json");
            }

            HttpResponseMessage apiResponseMessage = null;

            switch (apiRequest.ApiType)
            {
                case StaticDetails.ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;

                case StaticDetails.ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;

                case StaticDetails.ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;

                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            apiResponseMessage = await client.SendAsync(message);

            var apiContent = await apiResponseMessage.Content.ReadAsStringAsync();
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(apiContent);
            return apiResponseDTO;
        }
        catch (Exception ex)
        {
            var dto = new ResponseDTO()
            {
                DisplayMessage = "Error",
                ErrorMessages = new List<string>() { Convert.ToString(ex.Message) },
                IsSuccess = false
            };

            var res = JsonConvert.SerializeObject(dto);
            var apiResponseDTO = JsonConvert.DeserializeObject<T>(res);
            return apiResponseDTO;
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
