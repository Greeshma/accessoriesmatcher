using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Azure.Mobile.Server;
using Microsoft.Azure.Mobile.Server.Authentication;
using Microsoft.Azure.Mobile.Server.Config;
using AccessoriesMatcherService.DataObjects;
using AccessoriesMatcherService.Models;
using Owin;

namespace AccessoriesMatcherService
{
    public partial class Startup
    {
        public static void ConfigureMobileApp(IAppBuilder app)
        {
            HttpConfiguration config = new HttpConfiguration();

            //For more information on Web API tracing, see http://go.microsoft.com/fwlink/?LinkId=620686 
            config.EnableSystemDiagnosticsTracing();

            new MobileAppConfiguration()
                .UseDefaultConfiguration()
                .ApplyTo(config);

            //new Migrations.InitialCreate().Up();

            // Use Entity Framework Code First to create database tables based on your DbContext
            Database.SetInitializer(new AccessoriesMatcherInitializer());

            // To prevent Entity Framework from modifying your database schema, use a null database initializer
            // Database.SetInitializer<AccessoriesMatcherContext>(null);

            MobileAppSettingsDictionary settings = config.GetMobileAppSettingsProvider().GetMobileAppSettings();

            if (string.IsNullOrEmpty(settings.HostName))
            {
                // This middleware is intended to be used locally for debugging. By default, HostName will
                // only have a value when running in an App Service application.
                app.UseAppServiceAuthentication(new AppServiceAuthenticationOptions
                {
                    SigningKey = ConfigurationManager.AppSettings["SigningKey"],
                    ValidAudiences = new[] { ConfigurationManager.AppSettings["ValidAudience"] },
                    ValidIssuers = new[] { ConfigurationManager.AppSettings["ValidIssuer"] },
                    TokenHandler = config.GetAppServiceTokenHandler()
                });
            }
            app.UseWebApi(config);
        }
    }

    public class AccessoriesMatcherInitializer : CreateDatabaseIfNotExists<AccessoriesMatcherContext>
    {
        protected override void Seed(AccessoriesMatcherContext context)
        {
            List<Dress> todoItems = new List<Dress>
            {
                new Dress { Id = GenerateRandomId(), colour = "Red", userid = 100 },
                new Dress { Id = GenerateRandomId(), colour = "Blue", userid = 200 },
            };

            foreach (Dress todoItem in todoItems)
            {
                context.Set<Dress>().Add(todoItem);
            }

            base.Seed(context);
        }

        public string GenerateRandomId()
        {
            var rand = new Random();

            return rand.Next().ToString();
        }
    }
}

