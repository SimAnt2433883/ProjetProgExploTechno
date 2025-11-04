using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class LoginService
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyProdContext> factory;

        public LoginService(IDbContextFactory<Prog3A25AntoineTommyProdContext> factory)
        {
            this.factory = factory;
        }

        public async Task<Utilisateur?> VerifierConnexion(string email, string motPasse)
        {
            var db = await factory.CreateDbContextAsync();

            var emailParam = new SqlParameter("@email", email);
            var mdpParam = new SqlParameter("@mdp", motPasse);
            var reponseParam = new SqlParameter("@reponse", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            await db.Database.ExecuteSqlRawAsync(
                "EXEC Connexion @email, @mdp, @reponse OUTPUT",
                emailParam, mdpParam, reponseParam
            );

            int noUtilisateur = reponseParam.Value == DBNull.Value ? -1 : (int)reponseParam.Value;

            Console.WriteLine(noUtilisateur);

            if (noUtilisateur == -1)
                return null;

            List<Utilisateur> utilisateursLinq = 
                [.. from utilisateur in db.Utilisateurs
                    where utilisateur.NoUtilisateur == noUtilisateur
                    select utilisateur];

            Utilisateur? utilisateurRetour = null;

            foreach (Utilisateur utilisateurConnecte in utilisateursLinq)
                utilisateurRetour = utilisateurConnecte;

            return utilisateurRetour;
        }
    }
}
