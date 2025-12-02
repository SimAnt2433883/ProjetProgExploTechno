using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Prog3A25_AntoineTommy_Blazor.Components;
using Prog3A25_AntoineTommy_Blazor.Data;
using Prog3A25_AntoineTommy_Blazor.Services;

namespace Prog3A25_AntoineTommy_Blazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var conStrBuilder =
                new SqlConnectionStringBuilder(builder.Configuration.GetConnectionString("Connexion"))
                {
                    Password = builder.Configuration["MotPasse"]
                };
            builder.Services.AddPooledDbContextFactory<Prog3A25AntoineTommyContext>(
                x => x.UseSqlServer(conStrBuilder.ConnectionString));

            builder.Services.AddScoped<InscriptionService>();
            builder.Services.AddScoped<EditWikiService>();
            builder.Services.AddScoped<LoginService>();
            builder.Services.AddScoped<DonneeService>();
            builder.Services.AddScoped<WikiPlanteService>();
            builder.Services.AddScoped<WikiService>();
            builder.Services.AddScoped<ProtectedSessionStorage>();
            builder.Services.AddScoped<CustomAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddCascadingAuthenticationState();

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
