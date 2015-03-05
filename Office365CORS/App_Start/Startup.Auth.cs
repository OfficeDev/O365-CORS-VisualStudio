using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.IdentityModel.Tokens;

namespace Office365CORS
{
    public partial class Startup
    {
        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions
                {
                    Audience = SettingsHelper.ClientId,
                    Tenant = SettingsHelper.Tenant,
                    TokenValidationParameters = new TokenValidationParameters { SaveSigninToken = true }
                });
        }
    }
}