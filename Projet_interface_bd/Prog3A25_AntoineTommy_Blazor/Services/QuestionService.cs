using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Models;
using System;
using System.Data;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class QuestionService
    {
        private readonly IDbContextFactory<Prog3A25AntoineTommyContext> _factory;

        public QuestionService(IDbContextFactory<Prog3A25AntoineTommyContext> factory)
        {
            _factory = factory;
        }

        public async Task<List<QuestionUtilisateur>> GetQuestionsAsync()
        {
            await using var db = await _factory.CreateDbContextAsync();

            return await (
                from q in db.Questions
                join u in db.Utilisateurs
                    on q.NoUtilisateur equals u.NoUtilisateur
                orderby q.NoQuestion descending
                select new QuestionUtilisateur
                {
                    NoQuestion = q.NoQuestion,
                    Titre = q.Titre,
                    Question1 = q.Question1,
                    NoUtilisateur = (int)q.NoUtilisateur,
                    NomUtilisateur = u.Nom
                }
            ).ToListAsync();
        }
    }
}
