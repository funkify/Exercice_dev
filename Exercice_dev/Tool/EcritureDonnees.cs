namespace Exercice_Dev.Tools
{
    //Ecriture des fichiers 
    public static class EcritureDonnees
    {
        public static string TodayDate { get; set; }
        public static string CheminFichier { get; set; }

        static EcritureDonnees()
        {
            TodayDate = DateTime.Now.ToString("yyyyddMM");
            CheminFichier = "";
        }

        //Ecriture des données dans un fichier
        public static void FileBackup(List<DataFormat> dataFormatList)
        {
            CheminFichier = $"..\\..\\..\\Backup\\taxation.calls{TodayDate}";

            try
            {
                string logDirectory = Path.GetDirectoryName(CheminFichier);
                if (!Directory.Exists(logDirectory))
                {
                    Directory.CreateDirectory(logDirectory);
                }
                using (StreamWriter writer = new StreamWriter(CheminFichier))
                {
                    foreach (DataFormat format in dataFormatList)
                    {
                        writer.WriteLine($"{format.Date}${format.Heure}${format.DureeF}${format.Duree}${format.Type}${format.Depuis}${format.Vers}");
                    }
                }

                Logger.Log($"Écriture dans le fichier: {CheminFichier} terminée avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                Logger.Log($"Une erreur s'est produite : {ex.Message}");
            }
        }

        // Ecriture du fichier Sql
        public static void SqlRequest(List<DataFormat> dataFormatList)
        {
            CheminFichier = $"..\\..\\..\\Backup\\taxation_sql{TodayDate}.sql";

            try
            {
                using (StreamWriter writer = new StreamWriter(CheminFichier))
                {
                    foreach (DataFormat format in dataFormatList)
                    {
                        writer.WriteLine($"INSERT INTO  `detail_appels`(`id`, `date_appel`, `heure_appel`, `duree_appel`," +
                            $" `duree_appel_formate`, `cout`, `type_appel`, `numero_de`, `numero_vers`)" +
                            $" VALUES(NULL,{format.Date},{format.Heure},{format.Duree},{format.DureeF}, NULL,{format.Type},{format.Depuis},{format.Vers});"
                            );
                    }
                }
                Logger.Log($"Écriture du fichier sql: {CheminFichier} terminée avec succès.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Une erreur s'est produite : {ex.Message}");
                Logger.Log($"Une erreur s'est produite : {ex.Message}");
            }


        }



    }
}
