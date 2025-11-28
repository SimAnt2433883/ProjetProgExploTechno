using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Components.Pages;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.Net;
using System.Threading.Tasks;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class WikiService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> factory = factory;

        public async Task<List<Models.Wiki>> GetWikis()
        {
            var db = await factory.CreateDbContextAsync();

            List<Models.Wiki> wikis = [.. from wiki in db.Wikis
                                    select wiki];
            return wikis;
        }

        public async Task<List<Models.Wiki>> Filter(WikiFiltre wikiFiltre)
        {
            var db = await factory.CreateDbContextAsync();

            List<Models.Wiki> wikis = [.. from wiki in db.Wikis
                                          where (wikiFiltre.Temperature == null ||
                                                    (wikiFiltre.Temperature <= wiki.MaxTemperature &&
                                                     wikiFiltre.Temperature >= wiki.MinTemperature))
                                                &&
                                                (wikiFiltre.Humidite == null ||
                                                    (wikiFiltre.Humidite <= wiki.MaxHumidite &&
                                                     wikiFiltre.Humidite >= wiki.MinHumidite))
                                                &&
                                                (wikiFiltre.RayonsUV == null ||
                                                    (wikiFiltre.RayonsUV <= wiki.MaxRayonsUv &&
                                                     wikiFiltre.RayonsUV >= wiki.MinRayonsUv))

                                          select wiki];

            return wikis;
        }

        public List<Models.Wiki> Rechercher(List<Models.Wiki> wikis, string recherche)
        {
            recherche = recherche.ToLower();

            List<Models.Wiki> wikisFiltres = 
                string.IsNullOrWhiteSpace(recherche)
                ? wikis
                : [.. from wiki in wikis
                      where ((wiki.Nom ?? "")
                          .Contains(recherche, StringComparison.CurrentCultureIgnoreCase))
                      select wiki];

            return wikisFiltres;
        }
    }
}
