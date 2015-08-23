using System;
using System.Collections.Generic;
using AzureTableDataStore;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SimpleInjector;
using SimpleInjector.Diagnostics;

[assembly: OwinStartup(typeof(jutteleminen_sovellus.SignalR.Startup))]
namespace jutteleminen_sovellus.SignalR
{
    //public class Startup
    //{
    //    public void Configuration(IAppBuilder app)
    //    {
    //        // Any connection or hub wire up and configuration should go here
    //        app.MapSignalR();
    //    }
    //}

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var container = new Container();

            var registration = Lifestyle.Transient.CreateRegistration
            (
                typeof(ChatHub),
                () => new ChatHub(new ChatService()),
                container
            );

            container.AddRegistration(typeof(ChatHub), registration);
            registration.SuppressDiagnosticWarning
            (
                DiagnosticType.DisposableTransientComponent,
                "Ignoring IDisposable for transient hubs"
            );

            container.Verify();

            var config = new HubConfiguration()
            {
                Resolver = new ChatHubDependencyResolver(container)
            };

            app.MapSignalR(config);
        }
    }

    public class ChatHubDependencyResolver : DefaultDependencyResolver
    {
        private readonly Container _container;

        public ChatHubDependencyResolver(Container container)
        {
            _container = container;
        }

        public override object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container).GetService(serviceType)
                ?? base.GetService(serviceType);
        }

        public override IEnumerable<object> GetServices(Type serviceType)
        {
            /*
            return _container.GetAllInstances(serviceType)
                   .Concat(base.GetServices(serviceType));
                   */
            if (_container.GetRegistration(serviceType, false) != null)
            {
                return _container.GetAllInstances(serviceType);
            }
            else
            {
                return base.GetServices(serviceType);
            }
        }
    }
}
