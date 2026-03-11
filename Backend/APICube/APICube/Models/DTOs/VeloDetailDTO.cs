namespace APICube.Models.DTOs
{
    public class VeloArticleDTO
    {
        public int Idarticle { get; set; }
        public int IdCategorie { get; set; }
        public string Reference { get; set; }
        public decimal Prix { get; set; }
        public string Nomarticle { get; set; }
        public string? Description { get; set; }
        public decimal Poids { get; set; }
        public bool Disponibiliteenligne { get; set; }
        public string? Resume { get; set; }
        public decimal? Pourcentpromotion { get; set; }
        public int? Qtestock { get; set; }
    }

    public class VeloCouleurDTO
    {
        public int IdCouleur { get; set; }
        public string? Codehexacouleur { get; set; }
        public string? Effetpeinture { get; set; }
        public string? Nomcouleur { get; set; }
    }

    public class  VeloCadreDTO
    {
        public int Idmateriau { get; set; }
        public string? Nommateriau { get; set; }
        public string? Formecadre { get; set; }
    }

    public class VeloMillesimeDTO
    {
        public int Idmillesime { get; set; }
        public int Annee { get; set; }
    }

    public class VeloModeleDTO
    {
        public int Idmodele { get; set; }
        public string? Nommodele { get; set; }
    }

    public class VeloTailleDTO
    {
        public int Idtaille { get; set; }
        public string? Libelletaille { get; set; }
    }

    public class VeloUsageDTO
    {
        public int Idusage { get; set; }
        public string? Nomusage { get; set; }
    }


    public class VeloDetailDTO
    {
        public VeloDetailDTO() { }

        public string Reference { get; set; }
        public string Nomarticle { get; set; }
        public decimal Prix { get; set; }

        public VeloArticleDTO Article { get; set; }
        public VeloCouleurDTO Couleur { get; set; }
        public VeloCadreDTO Carde { get; set; }
        public VeloMillesimeDTO Millesime { get; set; }
        public VeloModeleDTO Modele { get; set; }
        public VeloTailleDTO Taille { get; set; }
        public VeloUsageDTO? Usage { get; set; }
    }
}
