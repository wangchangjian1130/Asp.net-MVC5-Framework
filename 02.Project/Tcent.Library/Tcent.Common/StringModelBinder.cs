using System.Web.Mvc;

namespace Tcent.Common
{
    /// <summary>
    /// MVC控制器，字符model绑定器
    /// </summary>
    /// { Created At Time:[ 2016/3/16 18:57 ], By User:wcj21259, On Machine:WCJ }
    /// <seealso cref="System.Web.Mvc.DefaultModelBinder" />
    public class StringModelBinder: DefaultModelBinder
    {
        /// <summary>
        /// Binds the model.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="bindingContext">The binding context.</param>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/23 14:11 ], By User:wcj21259, On Machine:WCJ }
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = base.BindModel(controllerContext, bindingContext);
            if (value is string)
            {
                return (value as string).Trim();
            }

            return value;
        }
    }
}
