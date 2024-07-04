using BiteBuddy.Web.Models;
using BiteBuddy.Web.Service.IService;
using Newtonsoft.Json;
using System.Text;
using static BiteBuddy.Web.Utility.SD;

namespace BiteBuddy.Web.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            this._httpClientFactory = httpClientFactory;
        }
        public async Task<ResponseDto?> GetAsync(RequestDto requestDto)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            //Create client from client factory
            HttpClient client = _httpClientFactory.CreateClient("CouponAPI");

            //Create Http Request Message and assign its properties
            HttpRequestMessage message = new();
            message.Headers.Add("Accept", "application/json");

            //Uri
            message.RequestUri = new Uri(requestDto.Url);
            if(requestDto.Data != null)
            {
             message.Content = new StringContent
                    (
                        JsonConvert.SerializeObject(requestDto.Data), 
                        Encoding.UTF8, 
                        "application/json"
                    );
            }

           

            switch(requestDto.ApiType)
            {
                case ApiType.GET:
                    message.Method = HttpMethod.Get;
                    break;

                case ApiType.POST:
                    message.Method = HttpMethod.Post;
                    break;

                case ApiType.PUT:
                    message.Method = HttpMethod.Put;
                    break;

                case ApiType.DELETE:
                    message.Method = HttpMethod.Delete;
                    break;

                    default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            //Create Http Response Message and assign its properties
            HttpResponseMessage? apiResponse = null;
            apiResponse = await client.SendAsync(message);
            try
            {
                switch (apiResponse.StatusCode)
                {
                    case System.Net.HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "Not Found" };
                        break;

                    case System.Net.HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "Unauthorised" };

                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync();
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                        return apiResponseDto;
                }
            }
            catch(Exception ex)
            {
                var dto = new ResponseDto
                {
                    Message = ex.Message,
                    IsSuccess = false
                };
                return dto;
            }
        }
    }
}
