using System.Net.Http.Json;
using System.Text.Json.Serialization;
using MauiStore.Web.Services;

namespace MauiStore.Shared.Services
{
    public sealed class MeetingService : BaseApiService
    {
        private const string MeetingTypesPath = "reuniones/tipos";
        private const string MeetingDepartmentsPath = "reuniones/departamentos/disponibles";
        private const string MeetingsPath = "reuniones";

        public MeetingService(ITokenStorageService tokenStorageService)
            : base(tokenStorageService)
        {
        }

        public async Task<List<MeetingTypeDto>> GetMeetingTypesAsync(CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.GetAsync(MeetingTypesPath, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error loading meeting types ({response.StatusCode}): {body}");
            }

            return await response.Content.ReadFromJsonAsync<List<MeetingTypeDto>>(cancellationToken: cancellationToken)
                ?? new List<MeetingTypeDto>();
        }

        public async Task<List<DepartmentDto>> GetDepartmentsAsync(CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.GetAsync(MeetingDepartmentsPath, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error loading departments ({response.StatusCode}): {body}");
            }

            var options = new System.Text.Json.JsonSerializerOptions 
            { 
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
            };

            return await response.Content.ReadFromJsonAsync<List<DepartmentDto>>(options, cancellationToken: cancellationToken)
                ?? new List<DepartmentDto>();
        }

        public async Task<List<MeetingSummaryDto>> GetMeetingsAsync(CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.GetAsync(MeetingsPath, cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new List<MeetingSummaryDto>();
                }

                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error loading meetings ({response.StatusCode}): {body}");
            }

            return await response.Content.ReadFromJsonAsync<List<MeetingSummaryDto>>(cancellationToken: cancellationToken)
                ?? new List<MeetingSummaryDto>();
        }

   

        public async Task<MeetingCreatedResponse> CreateMeetingAsync(
        MeetingCreateRequest request,
        CancellationToken cancellationToken = default)
            {
                await InitHttpClientHeaders();

                var response = await _http.PostAsJsonAsync(MeetingsPath, request, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.Content.ReadAsStringAsync(cancellationToken);
                    throw new HttpRequestException($"Error creating meeting ({response.StatusCode}): {body}");
                }

                return await response.Content.ReadFromJsonAsync<MeetingCreatedResponse>(
                    cancellationToken: cancellationToken)
                    ?? throw new HttpRequestException("No se recibió respuesta de la reunión creada.");
            }

        public async Task SendDraftAsync(int meetingId, CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.PatchAsync(
                $"{MeetingsPath}/{meetingId}/enviar",
                null,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error sending draft ({response.StatusCode}): {body}");
            }
        }

        public async Task<MeetingSummaryDto> GetMeetingByIdAsync(int meetingId, CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.GetAsync($"{MeetingsPath}/{meetingId}", cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error loading meeting ({response.StatusCode}): {body}");
            }

            return await response.Content.ReadFromJsonAsync<MeetingSummaryDto>(cancellationToken: cancellationToken)
                ?? throw new HttpRequestException("No se recibió información de la reunión.");
        }

        public async Task UpdateMeetingAsync(int meetingId, MeetingCreateRequest request, CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.PatchAsJsonAsync($"{MeetingsPath}/{meetingId}", request, cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error updating meeting ({response.StatusCode}): {body}");
            }
        }

        public async Task DiscardDraftAsync(int meetingId, CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.PatchAsync(
                $"{MeetingsPath}/{meetingId}/descartar",
                null,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error discarding draft ({response.StatusCode}): {body}");
            }
        }

        public async Task CancelMeetingAsync(int meetingId, CancellationToken cancellationToken = default)
        {
            await InitHttpClientHeaders();

            var response = await _http.PatchAsync(
                $"{MeetingsPath}/{meetingId}/cancelar",
                null,
                cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var body = await response.Content.ReadAsStringAsync(cancellationToken);
                throw new HttpRequestException($"Error canceling meeting ({response.StatusCode}): {body}");
            }
        }
    }

    public sealed class DepartmentDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nombre")]
        public string Nombre { get; set; } = string.Empty;
    }

    public sealed class MeetingTypeDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("descripcion")]
        public string Descripcion { get; set; } = string.Empty;
    }

    public sealed class MeetingSummaryDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("tipoReunion")]
        public string TipoReunion { get; set; } = string.Empty;

        [JsonPropertyName("departmentId")]
        public int DepartmentId { get; set; }

        [JsonPropertyName("departamento")]
        public string Departamento { get; set; } = string.Empty;

        [JsonPropertyName("estadoReunion")]
        public string EstadoReunion { get; set; } = string.Empty;

        [JsonPropertyName("motivo")]
        public string Motivo { get; set; } = string.Empty;

        [JsonPropertyName("observaciones")]
        public string Observaciones { get; set; } = string.Empty;

        [JsonPropertyName("fechaHoraInicio")]
        public DateTime FechaHoraInicio { get; set; }

        [JsonPropertyName("fechaHoraHasta")]
        public DateTime FechaHoraHasta { get; set; }
    }

    public sealed class MeetingCreateRequest
    {
        [JsonPropertyName("motivo")]
        public string Motivo { get; set; } = string.Empty;

        [JsonPropertyName("tipoReunion")]
        public string TipoReunion { get; set; } = string.Empty;

        [JsonPropertyName("departmentId")]
        public int DepartmentId { get; set; }

        [JsonPropertyName("observaciones")]
        public string Observaciones { get; set; } = string.Empty;

        [JsonPropertyName("fechaHoraInicio")]
        public string FechaHoraInicio { get; set; } = string.Empty;

        [JsonPropertyName("fechaHoraHasta")]
        public string FechaHoraHasta { get; set; } = string.Empty;

        [JsonPropertyName("isDraft")]
        public bool IsDraft { get; set; }
    }

    public sealed class MeetingCreatedResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
    }

}
