using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System.Net;
using System.Numerics;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class DonneeService
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> _factory;

        public DonneeService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<Donnee>> GetDonneesByUtilisateur(int noUtilisateur)
        {
            await using var db = await _factory.CreateDbContextAsync();

            // 1) trouver l'utilisateur
            var user = await db.Utilisateurs
                .FirstOrDefaultAsync(u => u.NoUtilisateur == noUtilisateur);

            if (user == null || user.NoPlante == null)
                return new List<Donnee>();

            // 2) récupérer les données correspondant à sa plante
            return await db.Donnees
                .Where(d => d.NoPlante == user.NoPlante)
                .OrderByDescending(d => d.DateHeure)
                .ToListAsync();
        }

        public async Task<Wiki?> GetBornesPlante(int noPlante)
        {
            await using var db = await _factory.CreateDbContextAsync();
            // On trouve la plante
            var plante = await db.Plantes
                .FirstOrDefaultAsync(p => p.NoPlante == noPlante);

            if (plante == null || plante.NoWiki == null)
                return null;

            // On récupère les bornes dans Wiki
            var bornes = from Wiki w in db.Wikis where w.NoWiki == plante.NoWiki select w;
            return await bornes.FirstOrDefaultAsync();
        }
    }
}
