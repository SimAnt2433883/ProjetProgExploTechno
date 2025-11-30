using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class EditWikiService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> factory = factory;

        private int VerifierChamps(EditWikiModel editWikiModel)
        {
            if (editWikiModel.Description == "" ||
                editWikiModel.LienImage == "" ||
                editWikiModel.Nom == "")
            {
                return -1;
            }
            else if (editWikiModel.TempMin >= editWikiModel.TempMax ||
                     editWikiModel.HumiditeMin >= editWikiModel.HumiditeMax ||
                     editWikiModel.RayonsUVMin >= editWikiModel.RayonsUVMax ||
                     editWikiModel.TempMin <= -100 ||
                     editWikiModel.TempMax >= 100 ||
                     editWikiModel.HumiditeMin < 0 ||
                     editWikiModel.HumiditeMax >= 100 ||
                     editWikiModel.RayonsUVMin < 0 ||
                     editWikiModel.RayonsUVMax >= 100)
            {
                return -2;
            }
            else if (!IsUrl(editWikiModel.LienImage))
                return -3;
            else if (editWikiModel.Description.Length > 1000)
                return -5;
            else if (editWikiModel.Nom.Length > 100)
                return -6;
            else if (editWikiModel.LienImage.Length > 1000)
                return -7;
            else
                return 0;

        }

        public async Task<int> CreerWiki(EditWikiModel editWikiModel)
        {
            int codeChamp = VerifierChamps(editWikiModel);
            if (codeChamp != 0)
                return codeChamp;

            var db = await factory.CreateDbContextAsync();

            Wiki wiki = new Wiki()
            {
                Nom = editWikiModel.Nom,
                Info = editWikiModel.Description,
                MinHumidite = editWikiModel.HumiditeMin,
                MaxHumidite = editWikiModel.HumiditeMax,
                MinTemperature = editWikiModel.TempMin,
                MaxTemperature = editWikiModel.TempMax,
                MinRayonsUv = editWikiModel.RayonsUVMin,
                MaxRayonsUv = editWikiModel.RayonsUVMax,
                ImagePlante = editWikiModel.LienImage
            };

            db.Wikis.Add(wiki);
            await db.SaveChangesAsync();

            return wiki.NoWiki;
        }

        public async Task<int> ModifierWiki(EditWikiModel editWikiModel, int id)
        {
            int codeChamp = VerifierChamps(editWikiModel);
            if (codeChamp != 0)
                return codeChamp;

            var db = await factory.CreateDbContextAsync();

            Wiki? wiki = await db.Wikis.FirstOrDefaultAsync(w => w.NoWiki == id);

            if (wiki == null)
                return -4;

            wiki.Nom = editWikiModel.Nom;
            wiki.Info = editWikiModel.Description;
            wiki.MinHumidite = editWikiModel.HumiditeMin;
            wiki.MaxHumidite = editWikiModel.HumiditeMax;
            wiki.MinTemperature = editWikiModel.TempMin;
            wiki.MaxTemperature = editWikiModel.TempMax;
            wiki.MinRayonsUv = editWikiModel.RayonsUVMin;
            wiki.MaxRayonsUv = editWikiModel.RayonsUVMax;
            wiki.ImagePlante = editWikiModel.LienImage;

            await db.SaveChangesAsync();

            return wiki.NoWiki;
        }


        private static bool IsUrl(string? input)
        {
            if (string.IsNullOrWhiteSpace(input)) return false;

            return Uri.TryCreate(input.Trim(), UriKind.Absolute, out var uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp 
                || uriResult.Scheme == Uri.UriSchemeHttps);
        }

        public async Task<EditWikiModel> GetModel(int id)
        {
            var db = await factory.CreateDbContextAsync();

            List<Wiki> wikis = [.. from wiki in db.Wikis
                                   where wiki.NoWiki == id
                                   select wiki];

            if (wikis.Count == 0)
                return null;

            Wiki wikiModel = wikis[0];

            EditWikiModel editWikiModel = new EditWikiModel()
            {
                Nom = wikiModel.Nom,
                LienImage = wikiModel.ImagePlante,
                Description = wikiModel.Info,
                TempMin = (int)wikiModel.MinTemperature,
                TempMax = (int)wikiModel.MaxTemperature,
                HumiditeMin = (int)wikiModel.MinHumidite,
                HumiditeMax = (int)wikiModel.MaxHumidite,
                RayonsUVMin = (int)wikiModel.MinRayonsUv,
                RayonsUVMax = (int)wikiModel.MaxRayonsUv
            };

            return editWikiModel;
        }

        public async Task<bool> SupprimerWiki(int id)
        {
            var db = await factory.CreateDbContextAsync();
            var wiki = await db.Wikis.FirstOrDefaultAsync(w => w.NoWiki == id);

            if (wiki == null)
                return false;

            db.Wikis.Remove(wiki);
            await db.SaveChangesAsync();
            return true;
        }

    }
}
