using Serilog;

namespace CityInfo.APIs.Extensions
{
    public static class LoggingExtensions
    {
        #region [ Serilog ]
        public static void ConfigureLoggingSetup(this IHostBuilder hostbuilder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.Console()
                .WriteTo.File("logs/cityinfo.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            hostbuilder.UseSerilog();
        }
        #endregion
    }
}
