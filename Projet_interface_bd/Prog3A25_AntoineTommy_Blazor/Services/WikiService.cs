using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Components.Pages;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.Data;
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

        public async Task<List<Models.Wiki>> GetWikisFiltres(FiltreWiki wikiFiltre)
        {
            var db = await factory.CreateDbContextAsync();

            List<Models.Wiki> wikis = [.. from wiki in db.Wikis
                                          where (wikiFiltre.Temperature <= wiki.MaxTemperature &&
                                                     wikiFiltre.Temperature >= wiki.MinTemperature) &&
                                                (wikiFiltre.Humidite <= wiki.MaxHumidite &&
                                                     wikiFiltre.Humidite >= wiki.MinHumidite) &&
                                                (wikiFiltre.RayonsUV <= wiki.MaxRayonsUv &&
                                                     wikiFiltre.RayonsUV >= wiki.MinRayonsUv)
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
                          .Contains(recherche, StringComparison.CurrentCultureIgnoreCase)) ||
                      ((wiki.Info ?? "")
                          .Contains(recherche, StringComparison.CurrentCultureIgnoreCase))
                      select wiki];

            return wikisFiltres;
        }

        public int UpdateIndexPage(int changement, int pagesTotales, int indexPageActuel)
        {
            int nouvelIndex = indexPageActuel + changement;

            if (nouvelIndex > pagesTotales)
                nouvelIndex = 1;

            if (nouvelIndex < 1)
                nouvelIndex = pagesTotales;

            return nouvelIndex;
        }


        public List<Models.Wiki> FiltrerWikis(Models.PageModel pageModel, List<Models.Wiki> wikisFiltres)
        {
            int debut = (pageModel.IndexPage - 1) * pageModel.TaillePage;
            return [.. wikisFiltres
                      .Skip(debut)                   // On saute les éléments avant la page
                      .Take(pageModel.TaillePage)];  // On prend TaillePage éléments de la page
        }

        public int GetNbPages(List<Models.Wiki> wikisFiltres, Models.PageModel pageModel)
        {
            return Math.Max(1,
                (int)Math.Ceiling((double)wikisFiltres.Count / pageModel.TaillePage));
        }

        public int CheckPage(Models.PageModel pageModel)
        {
            return Math.Clamp(pageModel.IndexPage, 1, pageModel.PagesTotales);
        }

        public async Task<string> GetRole(AuthenticationStateProvider authStateProvider)
        {
            var authState = await ((CustomAuthenticationStateProvider)authStateProvider).GetAuthenticationStateAsync();
            var user = authState.User;
            if (user.Identity.IsAuthenticated)
                return user.FindAll(System.Security.Claims.ClaimTypes.Role).Select(c => c.Value).ToList()[0];
            return "";
        }
    }
}
