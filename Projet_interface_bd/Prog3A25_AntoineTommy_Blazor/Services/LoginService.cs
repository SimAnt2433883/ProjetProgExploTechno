using Microsoft.AspNetCore.Components;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Prog3A25_AntoineTommy_Blazor.Authentication;
using System.Data;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class LoginService(IDbContextFactory<Prog3A25AntoineTommyProdContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyProdContext> factory = factory;

        public async Task<Utilisateur?> ConnecterUtilisateur(string email, string motPasse)
        {
            var db = await factory.CreateDbContextAsync();
            int? noUtilisateur = await ExecuterConnexion(db, email, motPasse);
            return GetUtilisateur(db, noUtilisateur);
        }


        private static async Task<int?> ExecuterConnexion(Prog3A25AntoineTommyProdContext db, string email, string motPasse)
        {
            var emailParam = new SqlParameter("@email", email);
            var mdpParam = new SqlParameter("@mdp", motPasse);
            var reponseParam = new SqlParameter("@reponse", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output
            };

            await db.Database.ExecuteSqlRawAsync(
                "EXEC Connexion @email, @mdp, @reponse OUTPUT",
                emailParam, mdpParam, reponseParam
            );

            return reponseParam.Value == DBNull.Value ? -1 : (int)reponseParam.Value;
        }


        private static Utilisateur? GetUtilisateur(Prog3A25AntoineTommyProdContext db, int? noUtilisateur)
        {
            List<Utilisateur> utilisateurs =
                [.. from utilisateur in db.Utilisateurs
                    where utilisateur.NoUtilisateur == noUtilisateur
                    select utilisateur];

            Utilisateur? utilisateurRetour = null;
            foreach (Utilisateur utilisateurConnecte in utilisateurs)
                utilisateurRetour = utilisateurConnecte;

            return utilisateurRetour;
        }

        //public static string GetRole(Utilisateur utilisateur)
        //{
        //    string role = "";

        //    if (utilisateur.Administrateur)
        //        role += "Administrateur";
        //    else
        //        role += "Normal";

        //    if (utilisateur.NoPlante != null)
        //        role += " plante";

        //    return role;
        //}
    }
}
