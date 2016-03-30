using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tcent.Domain
{
    /// <summary>
    /// 查询实体类
    /// </summary>
    /// { Created At Time:[ 2016/3/23 18:17 ], By User:wcj21259, On Machine:WCJ }
    public abstract class QueryModel
    {
        /// <summary>
        /// 页索引.
        /// </summary>
        /// <value>
        /// The index of the page.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:18 ], By User:wcj21259, On Machine:WCJ }
        public int? PageIndex { get; set; }

        /// <summary>
        /// 页容量.
        /// </summary>
        /// <value>
        /// The size of the page.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:18 ], By User:wcj21259, On Machine:WCJ }
        public int? PageSize { get; set; }

        /// <summary>
        /// 排序.
        /// </summary>
        /// <value>
        /// The order.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:18 ], By User:wcj21259, On Machine:WCJ }
        public string Order { get; set; }
    }

    /// <summary>
    /// 分页公共返回类型列表
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// { Created At Time:[ 2016/3/23 18:24 ], By User:wcj21259, On Machine:WCJ }
    public class BasePageList<T> where T : class, new()
    {
        public int? TotalCount { get; set; }

        /// <summary>
        /// 数据实体列表.
        /// </summary>
        /// <value>
        /// The model.
        /// </value>
        /// { Created At Time:[ 2016/3/23 18:26 ], By User:wcj21259, On Machine:WCJ }
        public IEnumerable<T> Models { get; set; }
    }
}
