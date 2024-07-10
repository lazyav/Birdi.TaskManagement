using System.Net;

namespace Birdi.TaskManagement.Application.Models
{
    public class ResponseObject
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;
        public bool IsSuccess { get; set; } = true;
        public string Error { get; set; } = string.Empty;
        public object Data { get; set; }
        public static ResponseObject Create(HttpStatusCode _statusCode = HttpStatusCode.OK, bool _isSuccess = true, string _error = "", object? _data = null)
        {
            return new ResponseObject()
            {
                StatusCode = _statusCode,
                IsSuccess = _isSuccess,
                Error = _error,
                Data = _data
            };
        }
    }
}
