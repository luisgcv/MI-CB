using MauiStore.Shared.Infrastructure.Interfaces;
using MauiStore.Shared.Services;
using Microsoft.JSInterop;

namespace MauiStore.Web.Services
{
    public sealed class WebFileService : IFileService
    {
        private readonly IJSRuntime _js;

        public WebFileService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<ApiBaseResponse<string>> SaveAndOpenAsync(
            byte[] fileBytes,
            string fileName,
            string contentType,
            CancellationToken ct = default)
        {
            try
            {
                var base64 = Convert.ToBase64String(fileBytes);

                await _js.InvokeVoidAsync(
                    "downloadFileFromBytes",
                    fileName,
                    contentType,
                    base64);

                return ApiBaseResponse<string>.Ok("Downloaded");
            }
            catch (Exception ex)
            {
                return ApiBaseResponse<string>.Failure(ex.Message);
            }
        }
    }
}
