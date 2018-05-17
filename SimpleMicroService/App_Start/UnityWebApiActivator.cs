using System.Web.Http;

using Unity.AspNet.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(SimpleMicroService.UnityWebApiActivator), nameof(SimpleMicroService.UnityWebApiActivator.Start))]
[assembly: WebActivatorEx.ApplicationShutdownMethod(typeof(SimpleMicroService.UnityWebApiActivator), nameof(SimpleMicroService.UnityWebApiActivator.Shutdown))]

namespace SimpleMicroService
{
    /// <summary>
    /// Provides the bootstrapping for integrating Unity with WebApi when it is hosted in ASP.NET.
    /// </summary>
    public static class UnityWebApiActivator
    {
        /// <summary>
        /// Integrates Unity when the application starts.
        /// </summary>
        public static void Start() 
        {
            // Use UnityHierarchicalDependencyResolver if you want to use
            // a new child container for each IHttpController resolution.
            // var resolver = new UnityHierarchicalDependencyResolver(UnityConfig.Container);

            var useUnityApp = UnityUtility.UnityApplication.GetApplication();
            var resolver = new UnityDependencyResolver(useUnityApp.CoreUnityContainer);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        /// <summary>
        /// Disposes the Unity container when the application is shut down.
        /// </summary>
        public static void Shutdown()
        {
            var useUnityApp = UnityUtility.UnityApplication.GetApplication();
            useUnityApp.CoreUnityContainer.Dispose();
        }
    }
}