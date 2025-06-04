using Newtonsoft.Json;
using OnlineRestaurant.MVC.Enums;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Service.IService;
using System.Net;
using System.Text;

namespace OnlineRestaurant.MVC.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("OnlineRestaurantAPI");

                HttpResponseMessage? apiResponse = null;

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = requestDto.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                apiResponse = await httpClient.SendAsync(message);
                var response = await apiResponse.Content.ReadAsStringAsync();

                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(response);
                    return apiResponseDto;
                }
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = response,
                    StatusCode = apiResponse.StatusCode
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    StatusCode = HttpStatusCode.InternalServerError
                };
            }

        }
    }
}
