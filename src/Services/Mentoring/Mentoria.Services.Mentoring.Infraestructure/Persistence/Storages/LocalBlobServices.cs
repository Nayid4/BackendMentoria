using Mentoria.Services.Mentoring.Application.Storage;
using Mentoria.Services.Mentoring.Application.Common.Dtos;
using ErrorOr;

namespace Mentoria.Services.Mentoring.Infraestructure.Persistence.Storages
{
    public class LocalBlobServices : IBlobService
    {
        private readonly string _rutaBase;
        private static readonly HashSet<string> _extensionesPermitidas = new HashSet<string>
        {
            ".pdf", ".doc", ".docx", ".jpg", ".jpeg", ".png", ".gif"
        };

        public LocalBlobServices(string rutaBase)
        {
            if (string.IsNullOrWhiteSpace(rutaBase))
                throw new ArgumentException("La ruta base no puede ser nula o vacía.", nameof(rutaBase));

            _rutaBase = rutaBase;

            // Crear el directorio base si no existe
            if (!Directory.Exists(_rutaBase))
            {
                Directory.CreateDirectory(_rutaBase);
            }
        }

        public async Task<ErrorOr<Guid>> UploadFileAsync(Stream stream, string nombreArchivo, CancellationToken cancellationToken = default)
        {
            if (stream == null || stream.Length == 0)
                return Error.Failure("ArchivoError", "El archivo está vacío o es nulo.");

            var extension = Path.GetExtension(nombreArchivo).ToLowerInvariant();

            // Validar la extensión del archivo
            if (!_extensionesPermitidas.Contains(extension))
                return Error.Failure("ArchivoError", $"La extensión '{extension}' no está permitida.");

            var idArchivo = Guid.NewGuid();
            var nombreConExtension = $"{idArchivo}{extension}";
            var rutaArchivo = Path.Combine(_rutaBase, nombreConExtension);

            try
            {
                // Escribir el archivo en el sistema local
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Create, FileAccess.Write, FileShare.None))
                {
                    await stream.CopyToAsync(fileStream, cancellationToken);
                }

                return idArchivo;
            }
            catch (IOException ex)
            {
                return Error.Failure("ArchivoError", $"Error de E/S al guardar el archivo: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Error.Failure("ArchivoError", $"Ocurrió un error al guardar el archivo: {ex.Message}");
            }
        }

        public async Task<ErrorOr<FileResponse>> DownloadFileAsync(Guid idArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                var archivos = Directory.GetFiles(_rutaBase, $"{idArchivo}.*");

                if (archivos.Length == 0)
                    return Error.Failure("ArchivoError", "El archivo no existe.");

                var rutaArchivo = archivos[0];
                var extension = Path.GetExtension(rutaArchivo).ToLowerInvariant();
                var stream = new MemoryStream();

                // Leer el archivo del sistema local
                using (var fileStream = new FileStream(rutaArchivo, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await fileStream.CopyToAsync(stream, cancellationToken);
                }

                // Resetear la posición del stream para que sea legible
                stream.Position = 0;

                // Determinar el tipo MIME basado en la extensión
                var contentType = ObtenerTipoMime(extension);

                return new FileResponse(stream, contentType);
            }
            catch (FileNotFoundException ex)
            {
                return Error.Failure("ArchivoError", $"Archivo no encontrado: {ex.Message}");
            }
            catch (IOException ex)
            {
                return Error.Failure("ArchivoError", $"Error de E/S al leer el archivo: {ex.Message}");
            }
            catch (Exception ex)
            {
                return Error.Failure("ArchivoError", $"Ocurrió un error al descargar el archivo: {ex.Message}");
            }
        }

        public Task<ErrorOr<Unit>> DeleteFileAsync(Guid idArchivo, CancellationToken cancellationToken = default)
        {
            try
            {
                var archivos = Directory.GetFiles(_rutaBase, $"{idArchivo}.*");

                if (archivos.Length == 0)
                    return Task.FromResult<ErrorOr<Unit>>(Error.Failure("ArchivoError", "El archivo no existe."));

                foreach (var archivo in archivos)
                {
                    if (File.Exists(archivo))
                    {
                        File.Delete(archivo);
                    }
                }

                return Task.FromResult<ErrorOr<Unit>>(Unit.Value);
            }
            catch (IOException ex)
            {
                return Task.FromResult<ErrorOr<Unit>>(Error.Failure("ArchivoError", $"Error de E/S al eliminar el archivo: {ex.Message}"));
            }
            catch (Exception ex)
            {
                return Task.FromResult<ErrorOr<Unit>>(Error.Failure("ArchivoError", $"Ocurrió un error al eliminar el archivo: {ex.Message}"));
            }
        }

        private string ObtenerTipoMime(string extension)
        {
            return extension switch
            {
                ".pdf" => "application/pdf",
                ".doc" => "application/msword",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                _ => "application/octet-stream", // Tipo MIME genérico para archivos desconocidos
            };
        }
    }
}
