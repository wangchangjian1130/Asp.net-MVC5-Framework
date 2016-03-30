using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Dependencies;
using System.Web.Http.Dispatcher;

namespace Tcent.Api.Web
{
    /// <summary>
    /// 依赖注册控制器激活类
    /// </summary>
    /// { Created At Time:[ 2016/3/10 16:49 ], By User:wcj21259, On Machine:WCJ }
    public class UnityHttpControllerActivator : IHttpControllerActivator
    {
        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="unityContainer">The unity container.</param>
        /// { Created At Time:[ 2016/3/10 16:50 ], By User:wcj21259, On Machine:WCJ }
        public UnityHttpControllerActivator(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        /// <summary>
        /// 依赖注入容器接口.
        /// </summary>
        /// <value>
        /// The unity container.
        /// </value>
        /// { Created At Time:[ 2016/3/10 16:51 ], By User:wcj21259, On Machine:WCJ }
        public IUnityContainer UnityContainer { get; private set; }

        /// <summary>
        /// 创建控制器.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <param name="controllerDescriptor">The controller descriptor.</param>
        /// <param name="controllerType">Type of the controller.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/10 16:53 ], By User:wcj21259, On Machine:WCJ }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            return (IHttpController)UnityContainer.Resolve(controllerType);
        }
    }

    /// <summary>
    /// 依赖解析器
    /// </summary>
    /// { Created At Time:[ 2016/3/10 16:55 ], By User:wcj21259, On Machine:WCJ }
    /// <seealso cref="System.Web.Http.Dependencies.IDependencyResolver" />
    public class UnityResolver : IDependencyResolver
    {
        /// <summary>
        /// 容器
        /// </summary>
        /// { Created At Time:[ 2016/3/10 16:55 ], By User:wcj21259, On Machine:WCJ }
        protected IUnityContainer Container;

        /// <summary>
        /// 构造函数.
        /// </summary>
        /// <param name="container">The container.</param>
        /// { Created At Time:[ 2016/3/10 16:58 ], By User:wcj21259, On Machine:WCJ }
        /// <exception cref="System.ArgumentNullException">container</exception>
        public UnityResolver(IUnityContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }

        /// <summary>
        /// 开启事务域.
        /// </summary>
        /// <returns>
        /// The dependency scope.
        /// </returns>
        /// { Created At Time:[ 2016/3/10 16:58 ], By User:wcj21259, On Machine:WCJ }
        /// <exception cref="System.NotImplementedException"></exception>
        public IDependencyScope BeginScope()
        {
            var child = Container.CreateChildContainer();
            return new UnityResolver(child);
        }

        /// <summary>
        /// 释放容器资源.
        /// </summary>
        /// { Created At Time:[ 2016/3/10 16:59 ], By User:wcj21259, On Machine:WCJ }
        public void Dispose()
        {
            Container.Dispose();
        }


        /// <summary>
        /// 获取容器中对应接口类型.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/10 17:00 ], By User:wcj21259, On Machine:WCJ }
        /// <exception cref="System.NotImplementedException"></exception>
        public object GetService(Type serviceType)
        {
            try
            {
                return Container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        /// <summary>
        /// 批量获取容器中对应接口类型.
        /// </summary>
        /// <param name="serviceType">Type of the service.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/10 17:00 ], By User:wcj21259, On Machine:WCJ }
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return Container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }
    }
}