using DarkSide;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System.Configuration;
using WebApplication2.Models;

namespace WebApplication2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<Concrete>(CreateConcrete);

            // Сначала настраиваем Identity, т.к. его тип могут понадобиться в типах предметной области
            ConfigureAuth(app);
            DomainConfiguration(app);
            //SignalRConfiguration(app);
        }
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationDbContext>(() => ApplicationDbContext.Create(ConnectionName));

            app.CreatePerOwinContext<UserManager>(UserManager.Create);

            app.CreatePerOwinContext<RoleManager>(RoleManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login")
            });
            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            //app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            //app.UseGoogleAuthentication();
        }
        //public void DomainConfiguration(IAppBuilder app)
        //{
        //    app.CreatePerOwinContext<ReservationManager>((IdentityFactoryOptions<ReservationManager> options, IOwinContext context) =>
        //    {
        //        ReservationManager rm = new ReservationManager(context.Get<Concrete>());

        //        return rm;
        //    });
        //}
        public void DomainConfiguration(IAppBuilder app)
        {
            app.CreatePerOwinContext<DogManager>((IdentityFactoryOptions<DogManager> options, IOwinContext context) =>
            {
                return new DogManager(context.Get<Concrete>());
            });
        }
        /// <summary>
        /// Имя строки соединения с БД
        /// </summary>
        public string ConnectionName
        {
            get
            {
                return ConfigurationManager.AppSettings["sys:connectionName"];
            }
        }

        /// <summary>
        /// Метод возвращает строку соединения по умолчанию
        /// </summary>
        /// <returns>Объект строки соединения</returns>
        public ConnectionStringSettings GetConnectionStringSettings()
        {
            return ConfigurationManager.ConnectionStrings[ConnectionName];
        }

        /// <summary>
        /// Создает объект управления подключением к БД
        /// </summary>
        public Concrete CreateConcrete(IdentityFactoryOptions<Concrete> options, IOwinContext context)
        {
            return new Concrete(GetConnectionStringSettings());
        }
    }
}