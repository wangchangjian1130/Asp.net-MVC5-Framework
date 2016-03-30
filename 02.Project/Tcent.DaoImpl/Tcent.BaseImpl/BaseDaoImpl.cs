using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tcent.IBase;

namespace Tcent.BaseImpl
{
    /// <summary>
    /// 基础数据接口实现类
    /// </summary>
    /// { Created At Time:[ 2016/3/10 18:15 ], By User:wcj21259, On Machine:WCJ }
    /// <seealso cref="Tcent.IBase.IBaseDao" />
    public class BaseDaoImpl : IBaseDao
    {
        /// <summary>
        /// Gets the list.
        /// </summary>
        /// <returns></returns>
        /// { Created At Time:[ 2016/3/10 18:21 ], By User:wcj21259, On Machine:WCJ }
        public List<string> GetList()
        {
            var list = new List<string>() { "a", "b", "c", "d", "e" };
            return list;
        }
    }
}
