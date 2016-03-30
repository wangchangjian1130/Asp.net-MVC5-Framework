using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcent.IBase
{
    /// <summary>
    /// 基础数据接口
    /// </summary>
    /// { Created At Time:[ 2016/3/10 18:15 ], By User:wcj21259, On Machine:WCJ }
    public interface IBaseDao
    {
        List<string> GetList();
    }
}
