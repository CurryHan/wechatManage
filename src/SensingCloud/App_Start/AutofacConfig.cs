using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using DI.Autofac.Modules;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MvcSiteMapProvider;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Xml;
using SensingCloud;
using Sensing.Data.Infrastructure;
using SensingCloud.Modules;
using Sensing.Entities.Users;
using Sensing.Data;
using Sensing.Data.Repository;
using SensingCloud.Services;
using SensingCloud.Authentication;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Owin;
using System.Web;
using SensingCloud.Helpers;


namespace SensingCloud
{
    public partial class Startup
    {
        public void AutoConfiguration(IAppBuilder app)
        {
            var builder = new ContainerBuilder();

            // Register your MVC controllers.
            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            // REGISTER DEPENDENCIES
            builder.RegisterType<SensingSiteDbContext>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationUserStore>().As<IUserStore<ApplicationUser>>().InstancePerRequest();
            builder.RegisterType<ApplicationUserManager>().AsSelf().InstancePerRequest();
            builder.RegisterType<RoleStore<IdentityRole>>().As<IRoleStore<IdentityRole, string>>().InstancePerRequest();
            builder.RegisterType<RoleManager<IdentityRole>>().AsSelf().InstancePerRequest();
            builder.RegisterType<ApplicationSignInManager>().AsSelf().InstancePerRequest();
            builder.Register<IAuthenticationManager>(c => HttpContext.Current.GetOwinContext().Authentication).InstancePerRequest();
            builder.Register<IDataProtectionProvider>(c => app.GetDataProtectionProvider()).InstancePerRequest();

            //builder.RegisterType<SensingSiteDbContext>().InstancePerRequest();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerRequest();
            builder.RegisterType<DatabaseFactory>().As<IDatabaseFactory>().InstancePerRequest();

            // Register modules
            builder.RegisterModule(new MvcSiteMapProviderModule()); // Required
            builder.RegisterModule(new MvcModule()); // Required by MVC. Typically already part of your setup (double check the contents of the module).


            builder.RegisterType<SiteMapNodeVisibilityProviderStrategy>()
                .As<ISiteMapNodeVisibilityProviderStrategy>()
                .WithParameter("defaultProviderName",
                    "MvcSiteMapProvider.FilteredSiteMapNodeVisibilityProvider,MvcSiteMapProvider");

            builder.RegisterType<ReservedAttributeNameProvider>()
                .As<IReservedAttributeNameProvider>()
                .WithParameter("attributesToIgnore", new string[]{"icon"});

            builder.RegisterAssemblyTypes(typeof(SoftwareUpdateRepository).Assembly)
                .Where(t => t.Name.EndsWith("Repository"))
                .AsImplementedInterfaces().InstancePerRequest();


            builder.RegisterAssemblyTypes(typeof(SoftwareUpdateService).Assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces().InstancePerRequest();

            builder.RegisterType<SMSHelper>().AsSelf().InstancePerRequest();
            builder.RegisterType<SmsService>().As<IIdentityMessageService>().InstancePerRequest();
            builder.RegisterType<EmailService>().As<IEmailService>().InstancePerRequest();

            builder.Register(c => new HttpContextWrapper(HttpContext.Current)).As<HttpContextBase>().InstancePerRequest();

            //builder.RegisterAssemblyTypes(typeof(DefaultFormsAuthentication).Assembly)
            //    .Where(t => t.Name.EndsWith("Authentication"))
            //    .AsImplementedInterfaces().InstancePerRequest();


            //builder.Register(c => new ApplicationUserManager(new UserStore<ApplicationUser>(new SensingSiteDbContext())))
            //    .As<ApplicationUserManager>().InstancePerRequest();

            //builder.Register(c => new ApplicationUserManager(new UserStore<ApplicationUser>(new SensingSiteDbContext())))
            //    .As<ApplicationUserManager>().InstancePerRequest();

            //builder.Register(c => new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new SensingSiteDbContext())))
            //    .As<UserManager<ApplicationUser>>().InstancePerRequest();


            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();


            



            // Create the DI container (typically part of your config already)

            



            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

            // REGISTER WITH OWIN
            app.UseAutofacMiddleware(container);
            app.UseAutofacMvc();


            // sitemap provider.
            // Setup global sitemap loader (required).
            MvcSiteMapProvider.SiteMaps.Loader = container.Resolve<ISiteMapLoader>();

            
            // Check all configured .sitemap files to ensure they follow the XSD for MvcSiteMapProvider (optional)
            var validator = container.Resolve<ISiteMapXmlValidator>();
            validator.ValidateXml(HostingEnvironment.MapPath("~/Mvc.sitemap"));

            // Register the Sitemaps routes for search engines (optional)
            XmlSiteMapController.RegisterRoutes(RouteTable.Routes);



            //var csl = new AutofacServiceLocator(container);
            //ServiceLocator.SetLocatorProvider(() => csl);

            //var csl = new AutofacServiceLocator(container);
            //ServiceLocator.SetLocatorProvider(() =>
            //{
            //    var httpContext = HttpContext.Current;
            //    if (httpContext.CurrentHandler is MvcHandler)
            //    {
            //        return new AutofacServiceLocator(AutofacDependencyResolver.Current.RequestLifetimeScope);
            //    }
            //    return csl;
            //});
            ConfigureAuth(app);
        }
    }
}