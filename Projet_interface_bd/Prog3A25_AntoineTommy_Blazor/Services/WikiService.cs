using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.Net;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class WikiService(IDbContextFactory<Prog3A25AntoineTommyProdContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyProdContext> factory = factory;

        public async Task<List<Wiki>> GetWikis()
        {
            var db = await factory.CreateDbContextAsync();

            List<Wiki> wikis = [.. from wiki in db.Wikis
                                    select wiki];
            return wikis;
        }

    }
}
