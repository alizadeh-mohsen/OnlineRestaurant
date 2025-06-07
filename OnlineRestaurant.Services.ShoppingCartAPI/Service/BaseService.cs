using Newtonsoft.Json;
using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;
using OnlineRestaurant.Services.ShoppingCartAPI.Service.IService;
using OnlineRestaurant.Services.ShoppingCartAPI.Utils;
using System.Net;
using System.Text;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service
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
                var client = _httpClientFactory.CreateClient("OnlineRestaurantAPI");

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
                message.Method = requestDto.ApiType switch
                {
                    ApiTypeEnum.POST => HttpMethod.Post,
                    ApiTypeEnum.PUT => HttpMethod.Put,
                    ApiTypeEnum.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                if (requestDto.Data != null)
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");

                message.RequestUri = new Uri(requestDto.Url);

                HttpResponseMessage response = await client.SendAsync(message);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (!string.IsNullOrEmpty(responseContent))
                    {
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(responseContent);
                        return apiResponseDto;
                    }
                }
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = responseContent ?? response.ReasonPhrase
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };
            }
        }
    }
}
