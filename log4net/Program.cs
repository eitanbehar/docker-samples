using log4net;
using log4net.Config;
using log4net.Util;

internal class Program
    {
    private static void Main(string[] args)
    {
        Console.WriteLine("log4net: Configuring...");

        var configurationFilePath = "log4net.xml";
        var configFile = new FileInfo(configurationFilePath);

        var loggerRepository = LogManager.GetAllRepositories().Any(x => x.Name == "main")
                ? LogManager.GetRepository("main")
                : LogManager.CreateRepository("main");

        var result = XmlConfigurator.ConfigureAndWatch(loggerRepository, configFile);

        if (result.Count > 0)
        {
                var errors = string.Join("\n", result.OfType<LogLog>());
                var firstException = result.OfType<LogLog>().Select(l => l.Exception).FirstOrDefault();
                throw new Exception($"Error reading log4net.xml configuration file {configFile.FullName}. {errors} {firstException}");
        }


        Console.WriteLine("log4net: Configured!");
    }
}