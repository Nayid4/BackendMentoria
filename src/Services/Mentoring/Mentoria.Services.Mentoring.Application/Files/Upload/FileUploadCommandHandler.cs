using Mentoria.Services.Mentoring.Application.Storage;

namespace Mentoria.Services.Mentoring.Application.Files.Upload
{
    public sealed class FileUploadCommandHandler : IRequestHandler<FileUploadCommand, ErrorOr<Guid>>
    {
        private readonly IBlobService _blobService;

        public FileUploadCommandHandler(IBlobService blobService)
        {
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }

        public async Task<ErrorOr<Guid>> Handle(FileUploadCommand command, CancellationToken cancellationToken)
        {
            // Validar que el archivo no sea nulo o vacío
            if (command.File == null || command.File.Length == 0)
                return Error.Failure("ArchivoError", "El archivo está vacío o es nulo.");

            // Validar el tipo de archivo (ContentType)
            var TypesAllowed = new[]
            {
                "application/pdf",
                "image/jpeg",
                "image/png",
                "application/msword",
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
            };

            if (!TypesAllowed.Contains(command.File.ContentType))
                return Error.Failure("ArchivoError", $"El tipo de archivo '{command.File.ContentType}' no está permitido.");

            // Validar tamaño del archivo (máximo 10 MB)
            const long sizeMax = 10 * 1024 * 1024; // 10 MB
            if (command.File.Length > sizeMax)
                return Error.Failure("ArchivoError", "El tamaño del archivo excede el límite permitido de 10 MB.");

            // Leer el archivo como stream y subirlo al servicio
            using Stream stream = command.File.OpenReadStream();
            var result = await _blobService.UploadFileAsync(stream, command.File.FileName, cancellationToken);

            // Retornar el resultado directamente con tipos explícitos
            return result.Match<ErrorOr<Guid>>(
                idArchivo => idArchivo, // Caso exitoso: devolver el GUID del archivo
                errors => errors // Caso de error: propagar los errores
            );
        }
    }
}
