using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Office365CORS.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Configuration;

namespace Office365CORS.Controllers
{
    [Authorize]
    public class DiscoveryResourceController : ApiController
    {
        // GET: api/DiscoveryResource
        public async Task<IEnumerable<DiscoveryResources>> Get()
        {
            ClientCredential clientCred = new ClientCredential(SettingsHelper.ClientId, SettingsHelper.AppKey);
            var bootstrapContext = ClaimsPrincipal.Current.Identities.First().BootstrapContext as System.IdentityModel.Tokens.BootstrapContext;
            string userAccessToken = bootstrapContext.Token;
            UserAssertion userAssertion = new UserAssertion(userAccessToken);

            AuthenticationContext authContext = new AuthenticationContext(string.Format("{0}/{1}", SettingsHelper.AuthorizationUri, SettingsHelper.Tenant));

            DiscoveryClient discClient = new DiscoveryClient(SettingsHelper.DiscoveryServiceEndpointUri,
                    () =>
                    {
                        var authResult = authContext.AcquireToken(SettingsHelper.DiscoveryServiceResourceId, clientCred, userAssertion);
                        return authResult.AccessToken;
                    });

            var dcr = await discClient.DiscoverCapabilitiesAsync();
            IEnumerable<DiscoveryResources> appCapabilities = dcr.Select(p => new DiscoveryResources { CapabilityName = p.Key, ServiceEndpointUri = p.Value.ServiceEndpointUri.ToString(), ServiceResourceId = p.Value.ServiceResourceId });
            return appCapabilities;
        }
    }
}
