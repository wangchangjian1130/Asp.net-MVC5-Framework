using System;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.Practices.Unity.Mvc;

namespace Tcent.Api.Web
{
    /// <summary>
    /// Specifies the Unity configuration for the main container.
    /// </summary>
    public class UnityConfig
    {
        /// <summary>
        /// 注册组件，依赖注入.
        /// </summary>
        /// { Created At Time:[ 2016/3/10 16:48 ], By User:wcj21259, On Machine:WCJ }
        public static void RegisterComponents()
        {
            DependencyResolver.SetResolver(new UnityDependencyResolver(GetConfiguredContainer()));
        }

        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            RegisterTypes(container);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion

        /// <summary>Registers the type mappings with the Unity container.</summary>
        /// <param name="container">The unity container to configure.</param>
        /// <remarks>There is no need to register concrete types such as controllers or API controllers (unless you want to 
        /// change the defaults), as Unity allows resolving a concrete type even if it was not previously registered.</remarks>
        public static void RegisterTypes(IUnityContainer container)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();

            // TODO: Register your types here
            // container.RegisterType<IProductRepository, ProductRepository>();

            var obj = ConfigurationManager.GetSection("unity");
            if (obj != null)
            {
                var section = (UnityConfigurationSection)obj;
                try
                {
                    section.Configure(container, "TcentContainer");
                }
                catch (Exception ex)
                {
                    throw new DllNotFoundException("unity配置发生异常", ex);
                }
            }
        }
    }
}
