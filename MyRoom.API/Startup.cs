using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using MyRoom.API.Providers;
using MyRoom.Data;
using MyRoom.Data.Providers;
using Owin;
using System;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Mvc;

[assembly: OwinStartup(typeof(MyRoom.API.Startup))]
namespace MyRoom.API
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }

        public static string PublicClientId { get; private set; }

        public void Configuration(IAppBuilder app)
        {

            AreaRegistration.RegisterAllAreas();

            HttpConfiguration config = new HttpConfiguration();

            ConfigureAuth(app);

            // Web API template created these 3            
            WebApiConfig.Register(config);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(config);

            // Tell WebApi to use our custom Ioc (Ninject)
            // IocConfig.RegisterIoc(GlobalConfiguration.Configuration);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
           // RouteConfig.RegisterRoutes(RouteTable.Routes);

            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);

        }

        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context and user manager to use a single instance per request
            app.CreatePerOwinContext(MyRoomDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseCookieAuthentication(new CookieAuthenticationOptions());
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Configure the application for OAuth based flow
            PublicClientId = "self";

            OAuthOptions = new OAuthAuthorizationServerOptions
            {

                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
              //  AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(1),
                Provider = new SimpleAuthorizationServerProvider()
               // RefreshTokenProvider = new SimpleRefreshTokenProvider()
            };

            // Token Generation
            app.UseOAuthAuthorizationServer(OAuthOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            //OAuthOptions = new OAuthAuthorizationServerOptions
            //{
            //    TokenEndpointPath = new PathString("/token"),
            //    Provider = new ApplicationOAuthProvider(PublicClientId),
            //    AuthorizeEndpointPath = new PathString("/api/Account/ExternalLogin"),
            //    AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
            //    AllowInsecureHttp = true
            //};


            // Enable the application to use bearer tokens to authenticate users
            app.UseOAuthBearerTokens(OAuthOptions);


            // Token Generation
            //app.UseOAuthAuthorizationServer(OAuthServerOptions);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
        }

    }
}