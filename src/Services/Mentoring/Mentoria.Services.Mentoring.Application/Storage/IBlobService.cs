using Mentoria.Services.Mentoring.Application.Common.Dtos;

namespace Mentoria.Services.Mentoring.Application.Storage
{
    public interface IBlobService
    {
        Task<ErrorOr<Guid>> UploadFileAsync(Stream stream, string contentType, CancellationToken cancellationToken = default);
        Task<ErrorOr<FileResponse>> DownloadFileAsync(Guid idArchivo, CancellationToken cancellationToken = default);
        Task<ErrorOr<Unit>> DeleteFileAsync(Guid idArchivo, CancellationToken cancellationToken = default);
    }
}
