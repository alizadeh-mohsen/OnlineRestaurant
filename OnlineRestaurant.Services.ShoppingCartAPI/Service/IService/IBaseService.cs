using OnlineRestaurant.Services.ShoppingCartAPI.Models;
using OnlineRestaurant.Services.ShoppingCartAPI.Models.Dto;

namespace OnlineRestaurant.Services.ShoppingCartAPI.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
