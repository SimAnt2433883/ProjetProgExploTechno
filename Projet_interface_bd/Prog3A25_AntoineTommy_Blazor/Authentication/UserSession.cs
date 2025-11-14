namespace Prog3A25_AntoineTommy_Blazor.Authentication
{
    public class UserSession
    {
        public string Nom { get; set; }
        public string Role { get; set; }
        public int NoUtilisateur { get; set; }
        public int? NoPlante { get; set; }
        public string Email { get; set; }


        public UserSession(string nom, string role, int noUtilisateur, int? noPlante, string email) 
        {
            Nom = nom;
            Role = role;
            NoUtilisateur = noUtilisateur;
            NoPlante = noPlante;
            Email = email;
        }
    }
}
