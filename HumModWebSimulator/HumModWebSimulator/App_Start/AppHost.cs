using System;
using System.Data;
using System.Linq;
using System.Configuration;
using System.Collections.Generic;
//using RestMasterService.ComputationNodes;
using Funq;
using HumModWebSimulator.Model;
using HumModWebSimulator.SimAppScreen;

using ServiceStack.Configuration;
using ServiceStack.OrmLite;
using ServiceStack.ServiceInterface;
using ServiceStack.ServiceInterface.Auth;
using ServiceStack.ServiceInterface.ServiceModel;
using ServiceStack.Text;
using ServiceStack.WebHost.Endpoints;

[assembly: WebActivator.PreApplicationStartMethod(typeof(HumModWebSimulator.App_Start.AppHost), "Start")]


/**
 * Entire ServiceStack Starter Template configured with a 'Hello' Web Service and a 'Todo' Rest Service.
 *
 * Auto-Generated Metadata API page at: /metadata
 * See other complete web service examples at: https://github.com/ServiceStack/ServiceStack.Examples
 */

namespace HumModWebSimulator.App_Start
{
	public class AppHost
		: AppHostBase
	{		
         //Tell Service Stack the name of your application and where to find your web services
            public AppHost() : base("SimApp Screen Definitions", typeof(SimAppScreenService).Assembly) { }

            public override void Configure(Container container)
            {
                //Set JSON web services to return idiomatic JSON camelCase properties
                JsConfig.EmitCamelCaseNames = true;

                SetConfig(new EndpointHostConfig {DefaultRedirectPath = "/WebApp/HumModBrowser.htm"});                

                OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;

                using (IDbConnection db = OpenDbConnection())
                {
                    db.CreateTable<SimAppScreenDTO>(false);
                    db.CreateTable<GraphicDTO>(false);
                }

                //Register all your dependencies

                container.Register(new SimAppScreenRepository());

                //TODO test - is it ok to upload from DB here - or from instance creation
                var myrepository = container.Resolve<SimAppScreenRepository>();
                myrepository.UploadFromDB();

                container.Register(new GraphicRepository());
                var mygrrepository = container.Resolve<GraphicRepository>();
                mygrrepository.UploadFromDB();
            }
		/* Uncomment to enable ServiceStack Authentication and CustomUserSession
		private void ConfigureAuth(Funq.Container container)
		{
			var appSettings = new AppSettings();

			//Default route: /auth/{provider}
			Plugins.Add(new AuthFeature(() => new CustomUserSession(),
				new IAuthProvider[] {
					new CredentialsAuthProvider(appSettings), 
					new FacebookAuthProvider(appSettings), 
					new TwitterAuthProvider(appSettings), 
					new BasicAuthProvider(appSettings), 
				})); 

			//Default route: /register
			Plugins.Add(new RegistrationFeature()); 

			//Requires ConnectionString configured in Web.Config
			var connectionString = ConfigurationManager.ConnectionStrings["AppDb"].ConnectionString;
			container.Register<IDbConnectionFactory>(c =>
				new OrmLiteConnectionFactory(connectionString, SqlServerDialect.Provider));

			container.Register<IUserAuthRepository>(c =>
				new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));

			var authRepo = (OrmLiteAuthRepository)container.Resolve<IUserAuthRepository>();
			authRepo.CreateMissingTables();
		}
		*/

		public static void Start()
		{
			new AppHost().Init();
		}

	    public static IDbConnection OpenDbConnection()
	    {
	        return ConfigurationManager.ConnectionStrings["RestMasterServiceDB"].ConnectionString.OpenDbConnection();
	    }
	}
}
