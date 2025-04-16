using Microsoft.AspNetCore.Http;

namespace Mentoria.Services.Mentoring.Application.Files.Upload
{
    public record FileUploadCommand(IFormFile File) : IRequest<ErrorOr<Guid>>;
}
