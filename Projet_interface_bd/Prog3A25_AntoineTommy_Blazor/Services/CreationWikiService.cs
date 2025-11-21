using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class CreationWikiService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> factory = factory;

        public async Task<int> CreerWiki(CreationWikiModel creationWikiModel)
        {
            if (creationWikiModel.Description == "" ||
                creationWikiModel.LienImage == "")
            {
                return -1;
            }
            else if (creationWikiModel.TempMin >= creationWikiModel.TempMax ||
                     creationWikiModel.HumiditeMin >= creationWikiModel.HumiditeMax ||
                     creationWikiModel.RayonsUVMin >= creationWikiModel.RayonsUVMax ||
                     creationWikiModel.TempMin <= -100 ||
                     creationWikiModel.TempMax >= 100 ||
                     creationWikiModel.HumiditeMin < 0 ||
                     creationWikiModel.HumiditeMax >= 100 ||
                     creationWikiModel.RayonsUVMin < 0 ||
                     creationWikiModel.RayonsUVMax >= 100)
            {
                return -2;
            }
            else if (!IsUrl(creationWikiModel.LienImage))
            {
                return -3;
            }

            var db = await factory.CreateDbContextAsync();

            Wiki wiki = new Wiki()
            {
                Info = creationWikiModel.Description,
                MinHumidite = creationWikiModel.HumiditeMin,
                MaxHumidite = creationWikiModel.HumiditeMax,
                MinTemperature = creationWikiModel.TempMin,
                MaxTemperature = creationWikiModel.TempMax,
                MinRayonsUv = creationWikiModel.RayonsUVMin,
                MaxRayonsUv = creationWikiModel.RayonsUVMax,
                ImagePlante = creationWikiModel.LienImage
            };

            db.Wikis.Add(wiki);
            await db.SaveChangesAsync();

            return wiki.NoWiki;
        }
        private static bool IsUrl(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            return Uri.TryCreate(input.Trim(), UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }

    }
}
