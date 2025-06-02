using OnlineRestaurant.MVC.Models;

namespace OnlineRestaurant.MVC.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto> SendAsync(RequestDto requestDto);
    }
}
