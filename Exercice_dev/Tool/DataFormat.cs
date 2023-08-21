namespace Exercice_Dev.Tools
{

    //Stocke et convertis les données au format demandé   
    public class DataFormat
    {
        public string? Date { get; set; }
        public string? Heure { get; set; }
        public string? Duree { get; set; }
        public string? Type { get; set; }
        public string? Depuis { get; set; }
        public string? Vers { get; set; }

        public string? DureeF
        {
            // Initialise le champs Durée Formaté , il contiendra le nombre de seconde.
            get
            {
                _dureeF = ConversionSec();
                return _dureeF;
            }
            set
            {
                _dureeF = value;
            }
        }

        private string? _dureeF;

        //Méthode qui convertis les heures / minutes en seconde
        public string ConversionSec()
        {
            if (Duree != null)
            {
                string[] separation = Duree.Split(":");

                int heures = int.Parse(separation[0]);
                int minute = int.Parse(separation[1]);
                int sec = int.Parse(separation[2]);

                int total = (heures * 3600) + (minute * 60) + sec;
                return $"{total}";
            }

            Logger.Log($"Error: Duree n'existe pas !");
            return $"Error: Duree n'existe pas !";
        }
    }
}
