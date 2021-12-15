using System.Collections.Generic;

using IdentityModel;

using IdentityServer4;
using IdentityServer4.Models;

namespace Notes.Identity
{
    public class Configuration
    {
        private const string ReactClientAppUri = "http://localhost:3000";

        private const string NotesWebApiScopeName = "NotesWebApi";

        public static IEnumerable<ApiScope> ApiScopes =>
            new List<ApiScope>()
            {
                new ApiScope(name: NotesWebApiScopeName, displayName: "Web API")
            };

        public static IEnumerable<ApiResource> ApiResources =>
            new List<ApiResource>()
            {
                new ApiResource(name: "NotesWebApi", displayName: "Web API", userClaims: new string[] { JwtClaimTypes.Name })
                {
                    Scopes = { NotesWebApiScopeName }
                }
            };

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>()
            {
                new Client()
                {
                    ClientId = "notes-web-api",
                    ClientName = "Notes Web",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,
                    RedirectUris =
                    {
                        $"{ReactClientAppUri}/signin-oidc"
                    },
                    AllowedCorsOrigins =
                    {
                        ReactClientAppUri
                    },
                    PostLogoutRedirectUris =
                    {
                        $"{ReactClientAppUri}/signout-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        NotesWebApiScopeName
                    },
                    AllowAccessTokensViaBrowser = true
                }
            };
    }
}
