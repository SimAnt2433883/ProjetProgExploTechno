using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Prog3A25_AntoineTommy_Blazor.Authentication;
using System.Security.Claims;

namespace Prog3A25_AntoineTommy_Blazor.Services
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private readonly ClaimsPrincipal _anonymous;

        public CustomAuthenticationStateProvider(ProtectedSessionStorage session)
        {
            _sessionStorage = session;
            _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var claimsPrincipal = _anonymous;
            try
            {
                var userSessionStorageResult =
                    await _sessionStorage.GetAsync<UserSession>("UserSession");
                var userSession =
                    userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession != null)
                {
                    claimsPrincipal =
                        new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userSession.Nom),
                            new Claim(ClaimTypes.Role, userSession.Role),
                            new Claim("NoUtilisateur", userSession.NoUtilisateur.ToString()),
                            new Claim("NoPlante", userSession.NoPlante.ToString()),
                            new Claim("Email", userSession.Email.ToString()),
                        }, "CustomAuth"));
                }
            }
            catch
            {
                claimsPrincipal = _anonymous;
            }

            return await Task.FromResult(new AuthenticationState(claimsPrincipal));
        }

        public async Task UpdateAuthenticationState(UserSession userSession)
        {
            ClaimsPrincipal claimsPrincipal;

            if (userSession != null)
            {
                await _sessionStorage.SetAsync("UserSession", userSession);
                claimsPrincipal =
                        new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userSession.Nom),
                            new Claim(ClaimTypes.Role, userSession.Role),
                            new Claim("NoUtilisateur", userSession.NoUtilisateur.ToString()),
                            new Claim("NoPlante", userSession.NoPlante.ToString()),
                            new Claim("Email", userSession.Email.ToString())
                        }, "CustomAuth"));
            }
            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(claimsPrincipal)));
        }
    }
}
