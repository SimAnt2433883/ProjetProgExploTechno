namespace Prog3A25_AntoineTommy_Blazor.Models
{
    public class EditWikiModel
    {
        public string Nom { get; set; } = "";
        public string LienImage { get; set; } = "";
        public string Description { get; set; } = "";
        public int TempMin { get; set; }
        public int TempMax { get; set; }
        public int HumiditeMin { get; set; }
        public int HumiditeMax { get; set; }
        public int RayonsUVMin { get; set; }
        public int RayonsUVMax { get; set; }
    }
}
