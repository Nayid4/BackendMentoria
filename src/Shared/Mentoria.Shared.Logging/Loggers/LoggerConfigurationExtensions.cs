

using Serilog;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.Graylog.Core.Transport;

namespace Mentoria.Shared.Logging.Loggers
{
    public static class LoggerConfigurationExtensions
    {
        public static LoggerConfiguration AddConsoleLogger(this LoggerConfiguration loggerConfiguration, ConsoleLoggerConfiguration consoleLoggerConfiguration)
        {
            return consoleLoggerConfiguration.Enabled
                ? loggerConfiguration.WriteTo.Console(consoleLoggerConfiguration.MinumLevel)
                : loggerConfiguration;
        }

        public static LoggerConfiguration AddGraylogLogger(this LoggerConfiguration loggerConfiguration, GraylogLoggerConfiguration graylogLoggerConfiguration)
        {
            return graylogLoggerConfiguration.Enabled
                ? loggerConfiguration.WriteTo.Graylog(new GraylogSinkOptions
                {
                    HostnameOrAddress = graylogLoggerConfiguration.Host,
                    Port = graylogLoggerConfiguration.Port,
                    TransportType = TransportType.Udp,
                    MinimumLogEventLevel = graylogLoggerConfiguration.MinimumLevel
                })
                : loggerConfiguration;
        }
    }
}
