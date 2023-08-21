using Exercice_Dev.Tools;


// Gestionnaire de log , le log se situe dans Log/log.txt , dans le dossier courant
Logger.Log("");
Logger.Log("");
Logger.Log("-------------------------");
Logger.Log("Démarrage du programme");
Logger.Log("-------------------------");
Logger.Log("Chargement des fichiers");
Logger.Log("-------------------------");

// Chargement des fichiers
List<FichierInfo> xmlPath = new List<FichierInfo>()
{
    new FichierInfo("TicketCollector_Call1.xml", 0),
    new FichierInfo("TicketCollector_Call2.xml", 1),
    new FichierInfo("TicketCollector_Call3.xml", 0)
};

Logger.Log("Chargements terminés !");

// Décryptage des données et préparation de la trame

Logger.Log("-------------------------");
Logger.Log("Décryptage des données");
Logger.Log("-------------------------");

XmlDcrypt dataDcrypt = new XmlDcrypt(xmlPath);
dataDcrypt.SimpleData();

Logger.Log("Extractions terminées !");

// Écriture  du fichier  

Logger.Log("-------------------------");
Logger.Log("Écriture du fichier");
Logger.Log("-------------------------");

EcritureDonnees.FileBackup(dataDcrypt.DataList);

Logger.Log("Écriture terminé !");

// Ecriture des requêtes sql 

Logger.Log("-------------------------");
Logger.Log("Écriture du fichier sql");
Logger.Log("-------------------------");

EcritureDonnees.SqlRequest(dataDcrypt.DataList);

Logger.Log("Écriture du fichier sql terminé !");


