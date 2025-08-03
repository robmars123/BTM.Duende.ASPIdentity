using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BTM.IdentityServer.BTM.Duende.ASPIdentity;

public static class Config
{
  public static IEnumerable<IdentityResource> IdentityResources =>
      new IdentityResource[]
      {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
      };

  public static IEnumerable<ApiResource> ApiResources =>
new ApiResource[]
   {
             new ApiResource("AccountAPI", "BTM Account API", new [] { "role" })
             {
                 Scopes = { "AccountAPI.fullaccess"},
                 ApiSecrets = { new Secret("mysecret".Sha256()) }
             },
             new ApiResource("ProductsAPI", "BTM Products API", new [] { "role" })
             {
                 Scopes = { "ProductsAPI.fullaccess"},
                 ApiSecrets = { new Secret("mysecret".Sha256()) }
             }
   };

  public static IEnumerable<ApiScope> ApiScopes =>
  new ApiScope[]
      {
                //clients should match these scopes
                new ApiScope("AccountAPI.fullaccess"),
                new ApiScope("ProductsAPI.fullaccess")};

  public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client()
                {
                    ClientName = "Account Service Web Client",
                    //this is the client id that will be used by the client application to identify itself
                    //BTM.Account.MVC.Client
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AccessTokenLifetime = 120,
                    ClientId = "Account.MVC.Client",
                    AllowedGrantTypes = GrantTypes.Code,
                                        RedirectUris = new List<string>()
                    {
                        "https://localhost:7282/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7282/signout-callback-oidc"
                    } ,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "AccountAPI.fullaccess",//gains access to AccountAPI
                        "ProductsAPI.fullaccess", //gains access to ProductsAPI
                        "offline_access"
                    },
                    ClientSecrets =
                    {
                        new Secret("mysecret".Sha256())
                    },
            },
            new Client
            {
                ClientId = "swagger",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials, // or AuthorizationCode, ClientCredentials, etc.
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedScopes = { "AccountAPI.fullaccess", "ProductsAPI.fullaccess" }
            }
        };
}
