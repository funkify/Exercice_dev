using System.Xml;

namespace Exercice_Dev.Tools
{
    //Contrôle et charge les fichiers XML 
    public class FichierInfo
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public int Type { get; set; }

        public XmlDocument? DataXml { get; set; }

        public FichierInfo(string filename, int type = 0)
        {
            FileName = filename;
            Type = type;
            Path = $"..\\..\\..\\XmlFile\\{FileName}";

            //Charge les xml dès l'instanciation de la classe
            CheckOpen();

            // Supprime l'encodage erroné des fichiers de Type 1
            if (Type == 1)
            {
                string fileContent = File.ReadAllText(Path);
                fileContent = fileContent.Replace(" encoding=\"GBK\"", "");
                File.WriteAllText(Path, fileContent);
                Logger.Log($"Suppression de l'encoding erroné GBK pour le fichier: {FileName}");
            }
        }
        public void CheckOpen()
        {
            try
            {
                DataXml = new XmlDocument();
                DataXml.Load(Path);
                Logger.Log($"Chargement du fichier xml {FileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur dans le fichier {Path} - {ex.Message} ");
                Logger.Log($"Erreur dans le fichier {Path} - {ex.Message} ");
            }
        }
    }
}

