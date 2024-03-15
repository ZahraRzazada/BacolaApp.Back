using System;
namespace Bacola.Service.Responses
{
    public class CustomResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}

