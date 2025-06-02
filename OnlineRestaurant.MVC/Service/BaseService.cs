using Newtonsoft.Json;
using OnlineRestaurant.MVC.Enums;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Service.IService;
using System;
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

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");

                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage? apiResponse = null;

                message.Method = requestDto.ApiType switch
                {
                    ApiType.POST => HttpMethod.Post,
                    ApiType.PUT => HttpMethod.Put,
                    ApiType.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                apiResponse = await httpClient.SendAsync(message);

                if (apiResponse.IsSuccessStatusCode)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                    return apiResponseDto;
                }
                var errorResponse = await apiResponse.Content.ReadAsStringAsync();
                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = errorResponse,
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
