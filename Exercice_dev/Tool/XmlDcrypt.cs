namespace Exercice_Dev.Tools
{
    //Extrait les données des fichiers XML
    public class XmlDcrypt
    {
        //Récupère les xml ainsi que leur type (les types servent à reconnaître les xml avec une nomenclatrure différente )
        public List<FichierInfo> FichierList { get; set; }

        //On utilise la classe DataFormat pour gérer les modifications faites sur les données brut 
        public List<DataFormat> DataList { get; set; }

        public XmlDcrypt(List<FichierInfo> fichierList)
        {
            FichierList = fichierList;
            DataList = new List<DataFormat>();
        }

        // Méthode d'extraction des données bruts
        public void SimpleData()
        {
            foreach (FichierInfo fichier in FichierList)
            {
                DataFormat data = new DataFormat();

                if (fichier.DataXml != null)
                {
                    Logger.Log($"Extraction des données du fichier: {fichier.FileName}");
                    // Gestions des extractions selon le type de fichier XML
                    switch (fichier.Type)
                    {
                        // TicketCollector_Call1 et 3 
                        case 0:
                            data.Date = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/Date")?.InnerText ?? "";
                            data.Heure = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/Time")?.InnerText ?? "";
                            data.Duree = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/CallDuration")?.InnerText ?? "";
                            data.Type = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/CommunicationType")?.InnerText ?? "";

                            // Inverse les "depuis" et "vers" selon si l'appel est entrant ou sortant 
                            if (data.Type == "Outgoing")
                            {
                                data.Depuis = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/ChargedUserID")?.InnerText ?? "";
                                data.Vers = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/DialledNumber")?.InnerText ?? "";
                            }
                            else
                            {
                                data.Depuis = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/DialledNumber")?.InnerText ?? "";
                                data.Vers = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/ChargedUserID")?.InnerText ?? "";
                            }

                            break;

                        //TickectCollector_Call2
                        case 1:
                            string fullSTime = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/DateOfBegin")?.InnerText ?? "";
                            string fullETime = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/DateOfEnd")?.InnerText ?? "";

                            DateTime dateB = DateTime.ParseExact(fullSTime, "yyyy-MM-ddTHH:mm:ss", null);
                            DateTime dateE = DateTime.ParseExact(fullETime, "yyyy-MM-ddTHH:mm:ss", null);
                            TimeSpan difference = dateE - dateB;

                            data.Date = dateB.ToString("yyyy-MM-dd");
                            data.Heure = dateB.ToString("HH:mm:ss");
                            data.Duree = difference.ToString(@"hh\:mm\:ss");
                            data.Type = "Local";
                            data.Depuis = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/DirNumber")?.InnerText ?? "";
                            data.Vers = fichier.DataXml.SelectSingleNode("CallAccountingList/CallAccounting/CorrNumber")?.InnerText ?? "";
                            break;

                    }
                }

                //Enregistrements des données 
                DataList.Add(data);
                Logger.Log("Données extraites !");

            }
        }

        //Afficher les données enregistrées 
        public void AfficheDatalist()
        {
            foreach (DataFormat item in DataList)
            {
                Console.WriteLine("----------------");
                Console.WriteLine($"Date : {item.Date}");
                Console.WriteLine($"Heure : {item.Heure}");
                Console.WriteLine($"Duree : {item.Duree}");
                Console.WriteLine($"Duree formaté : {item.DureeF}");
                Console.WriteLine($"Type : {item.Type}");
                Console.WriteLine($"Depuis : {item.Depuis}");
                Console.WriteLine($"Vers : {item.Vers}");
                item.ConversionSec();
                Console.WriteLine("----------------");
            }
        }

        //Afficher les données xml bruts des fichiers chargés 
        public void Affiche()
        {
            foreach (FichierInfo fichier in FichierList)
            {
                if (fichier.DataXml != null)
                    Console.WriteLine(fichier.DataXml.InnerText);
            }
        }
    }
}
