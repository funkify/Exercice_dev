public static class Logger
{
    private static readonly string logFilePath = "..\\..\\..\\Log\\log.txt";

    public static void Log(string message)
    {
        try
        {
            using (StreamWriter logWriter = new StreamWriter(logFilePath, true))
            {
                string logMessage = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}: {message}";
                logWriter.WriteLine(logMessage);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erreur de journalisation : {ex.Message}");
        }
    }
}
