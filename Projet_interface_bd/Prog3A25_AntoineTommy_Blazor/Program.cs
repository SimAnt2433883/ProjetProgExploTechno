using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Components;
using Prog3A25_AntoineTommy_Blazor.Data;

// ajouter comme paramètre aux constructeurs de services :
// public UserAccountService(IDbContextFactory<Prog3A25AntoineTommyProdContext> factory)

namespace Prog3A25_AntoineTommy_Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var conStrBuilder =
                new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Connexion"));
            conStrBuilder.Password = builder.Configuration["MotPasse"];
            builder.Services.AddPooledDbContextFactory<Prog3A25AntoineTommyProdContext>(
                x => x.UseSqlServer(conStrBuilder.ConnectionString));
            // Add services to the container.
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
