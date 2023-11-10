namespace FolderCleanupService;

public class Logger : ILogger
{
    private readonly string _logFilePath;

    public Logger()
    {
        _logFilePath = "./log.txt";

        if (File.Exists(_logFilePath))
        {
            File.Delete(_logFilePath);
        }
    }

    public void Log(string message)
    {
        File.AppendAllText(_logFilePath, $"{DateTime.Now}: {message}{Environment.NewLine}");
    }
}
