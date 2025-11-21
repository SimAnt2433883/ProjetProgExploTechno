using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.Data;
using Microsoft.Data.SqlClient;


namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class InscriptionService
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> _factory;

        public InscriptionService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
        {
            _factory = factory;
        }

        public async Task<Utilisateur> InscrireUtilisateur(string email, string nom, string motPasse)
        {
            try
            {
                using var db = await _factory.CreateDbContextAsync();

                var emailParam = new SqlParameter("@email", email);
                var nomParam = new SqlParameter("@nom", nom);
                var mdpParam = new SqlParameter("@motPasse", motPasse);
                var reponseParam = new SqlParameter("@reponse", System.Data.SqlDbType.Int)
                {
                    Direction = System.Data.ParameterDirection.Output
                };

                await db.Database.ExecuteSqlRawAsync(
                    "EXEC Inscription @nom, @email, @motPasse, @reponse OUTPUT",
                    nomParam, emailParam, mdpParam, reponseParam
                );

                int noUtilisateur = reponseParam.Value == DBNull.Value ? -1 : (int)reponseParam.Value;

                List<Utilisateur> utilisateurs =
                [.. from utilisateur in db.Utilisateurs
                    where utilisateur.NoUtilisateur == noUtilisateur
                    select utilisateur];

                Utilisateur? utilisateurRetour = null;
                foreach (Utilisateur utilisateurConnecte in utilisateurs)
                    utilisateurRetour = utilisateurConnecte;

                return utilisateurRetour;
            }
            catch
            {
                return null;
            }
        }
    }
}
