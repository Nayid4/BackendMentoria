
using Mentoria.Services.Mentoring.Application.Common.Dtos;

namespace Mentoria.Services.Mentoring.Application.Files.Download
{
    public record FileDownloadQuery(Guid Id) : IRequest<ErrorOr<FileResponse>>;
}
