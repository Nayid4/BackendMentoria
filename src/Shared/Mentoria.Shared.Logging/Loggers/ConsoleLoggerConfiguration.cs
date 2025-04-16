

using Serilog.Events;

namespace Mentoria.Shared.Logging.Loggers
{
    public class ConsoleLoggerConfiguration
    {
        public bool Enabled { get; set; } = false;
        public LogEventLevel MinumLevel { get; set; }
    }
}
