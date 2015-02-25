using Microsoft.Owin.Security.ActiveDirectory;
using Owin;
using System.Configuration;
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
                    Audience = SettingsHelper.ClientId,// ConfigurationManager.AppSettings["ida:ClientID"],
                    Tenant = SettingsHelper.Tenant, //ConfigurationManager.AppSettings["ida:Tenant"],
                    MetadataAddress = "https://login.windows-ppe.net/cbe307ce-324e-43ed-85e6-b4e11629e516/federationmetadata/2007-06/federationmetadata.xml",
                    TokenValidationParameters = new TokenValidationParameters { SaveSigninToken = true }
                });
        }
    }
}