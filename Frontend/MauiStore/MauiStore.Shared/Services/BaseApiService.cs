using MauiStore.Web.Services;

namespace MauiStore.Shared.Services
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient _http;
        protected readonly ITokenStorageService _tokenStorageService;
        public BaseApiService(ITokenStorageService tokenStorageService)
        {
            _http = CreateUnsafeClient();
            _tokenStorageService = tokenStorageService;
        }

        protected static HttpClient CreateUnsafeClient()
        {
            var handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback =
                HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };

            //aca va el url de la api
            return new HttpClient(handler) { BaseAddress = new Uri("http://192.168.100.185:3000/") };
        }

        protected async Task InitHttpClientHeaders()
        {
            if (!_http.DefaultRequestHeaders.Any(header => header.Key == "client-id"))
                _http.DefaultRequestHeaders.Add("client-id", await _tokenStorageService.GetClientId());
        }
    }
    public class ApiBaseResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public static ApiBaseResponse<T> Failure(string? message)
        {
            return new ApiBaseResponse<T>
            {
                Success = false,
                Message = message,
                Data = default
            };
        }
        public static ApiBaseResponse<T> Ok(T data)
        {
            return new ApiBaseResponse<T>
            {
                Success = true,
                Message = null,
                Data = data
            };
        }
    }


    public class ApiBaseResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public static ApiBaseResponse Failure(string? message)
        {
            return new ApiBaseResponse
            {
                Success = false,
                Message = message,
            };
        }
        public static ApiBaseResponse Ok()
        {
            return new ApiBaseResponse
            {
                Success = true,
                Message = null,
            };
        }
    }
}
