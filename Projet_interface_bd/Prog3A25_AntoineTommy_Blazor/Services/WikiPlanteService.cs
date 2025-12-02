using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class WikiPlanteService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> factory = factory;

        public async Task<Wiki?> GetWiki(int noWiki)
        {
            var db = await factory.CreateDbContextAsync();

            List<Wiki> wikis =
                [.. from wiki in db.Wikis
                    where wiki.NoWiki == noWiki
                    select wiki];

            Wiki? wikiRetour = null;
            foreach (Wiki wiki in wikis)
                wikiRetour = wiki;

            return wikiRetour;
        }

        public async Task<int> GetMaxId()
        {
            var db = await factory.CreateDbContextAsync();

            int? resultat =
                await (from w in db.Wikis
                       select w.NoWiki).MaxAsync();

            return resultat ?? 0;
        }


        public async Task<int> AllerAuWiki(int id, int ajout, int maxId)
        {
            int wikiCible = IncrementerWiki(id, ajout, maxId);

            while (true) // tant que le wiki est invalide, on check le prochain
            {
                Models.Wiki? wiki = await GetWiki(wikiCible);

                if (wiki != null)
                    break;

                wikiCible = IncrementerWiki(wikiCible, ajout, maxId);
            }

            return wikiCible;
        }

        private static int IncrementerWiki(int valeur, int ajout, int maxId)
        {
            valeur += ajout;

            if (valeur > maxId)
                valeur = 1;
            else if (valeur < 1)
                valeur = maxId;

            return valeur;
        }

    }
}
