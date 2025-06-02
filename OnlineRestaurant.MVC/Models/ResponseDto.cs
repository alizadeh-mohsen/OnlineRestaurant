using System.Net;

namespace OnlineRestaurant.MVC.Models
{
    public class ResponseDto
    {
        public object? Result { get; set; }
        public bool IsSuccess { get; set; } = true;
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK; // Default to 200 OK+
    }
}
