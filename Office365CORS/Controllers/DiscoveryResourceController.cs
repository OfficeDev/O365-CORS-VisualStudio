﻿//----------------------------------------------------------------------------------------------
//    Copyright 2015 Microsoft Corporation
//
//    Licensed under the MIT License (MIT);
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
//
//      http://mit-license.org/
//
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
//----------------------------------------------------------------------------------------------

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Office365.Discovery;
using Office365CORS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;

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
