using MauiStore.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiStore.Shared.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<ApiBaseResponse<string>> SaveAndOpenAsync(
            byte[] fileBytes,
            string fileName,
            string contentType,
            CancellationToken ct = default);
    }
}
