using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MauiStore.Shared.Infrastructure.Interfaces;
using MauiStore.Shared.Services;
using Microsoft.Maui.Storage;
namespace MauiStore.Services
{
    public sealed class MauiFileService : IFileService
    {
        public async Task<ApiBaseResponse<string>> SaveAndOpenAsync(
            byte[] fileBytes,
            string fileName,
            string contentType,
            CancellationToken ct = default)
        {
            try
            {
                var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                await File.WriteAllBytesAsync(filePath, fileBytes, ct);

                await Launcher.Default.OpenAsync(new OpenFileRequest
                {
                    File = new ReadOnlyFile(filePath)
                });

                return ApiBaseResponse<string>.Ok(filePath);
            }
            catch (Exception ex)
            {
                return ApiBaseResponse<string>.Failure(ex.Message);
            }
        }
    }

}
