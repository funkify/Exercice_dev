public static class Logger
{
    private static readonly string CheminFichier = "..\\..\\..\\Log\\log.txt";

    public static void Log(string message)
    {
        try
        {
            string logDirectory = Path.GetDirectoryName(CheminFichier);
            if (!Directory.Exists(logDirectory))
            {
                Directory.CreateDirectory(logDirectory);
            }

            using (StreamWriter logWriter = new StreamWriter(CheminFichier, true))
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
