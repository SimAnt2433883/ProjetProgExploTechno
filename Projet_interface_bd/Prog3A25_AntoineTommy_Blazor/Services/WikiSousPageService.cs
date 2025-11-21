using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class WikiSousPageService(IDbContextFactory<Prog3A25AntoineTommyProdContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyProdContext> factory = factory;

        public async Task<Wiki> GetWiki(int noWiki)
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

        public async Task<bool> EstValide(int id)
        {
            var db = await factory.CreateDbContextAsync();

            List<Wiki> wikis =
                [.. from wiki in db.Wikis
                    select wiki];

            return id > 0 && id <= wikis.Count;
        }
    }
}
