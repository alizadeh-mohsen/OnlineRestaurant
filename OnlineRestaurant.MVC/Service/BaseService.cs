using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OnlineRestaurant.MVC.Models;
using OnlineRestaurant.MVC.Service.IService;
using OnlineRestaurant.MVC.Utils;
using System.Net;
using System.Text;

namespace OnlineRestaurant.MVC.Service
{
    public class BaseService : IBaseService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ITokenProvider _tokenProvider;

        public BaseService(IHttpClientFactory httpClientFactory, ITokenProvider tokenProvider)
        {
            _httpClientFactory = httpClientFactory;
            _tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto> SendAsync(RequestDto requestDto, bool sendToken = true)
        {
            try
            {
                var httpClient = _httpClientFactory.CreateClient("OnlineRestaurantAPI");

                HttpResponseMessage? apiResponse = null;

                HttpRequestMessage message = new HttpRequestMessage();
                message.Headers.Add("Accept", "application/json");
              
                if(sendToken)
                {
                    var token = _tokenProvider.GetToken();
                    if (!string.IsNullOrEmpty(token))
                    {
                        message.Headers.Add("Authorization", $"Bearer {token}");
                    }
                }
                message.RequestUri = new Uri(requestDto.Url);
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data),
                        Encoding.UTF8, "application/json");
                }

                message.Method = requestDto.ApiType switch
                {
                    ApiTypeEnum.POST => HttpMethod.Post,
                    ApiTypeEnum.PUT => HttpMethod.Put,
                    ApiTypeEnum.DELETE => HttpMethod.Delete,
                    _ => HttpMethod.Get
                };

                apiResponse = await httpClient.SendAsync(message);
                if (apiResponse.StatusCode == HttpStatusCode.OK)
                {
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent);

                    return apiResponseDto;
                }

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = apiResponse.ReasonPhrase,
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
