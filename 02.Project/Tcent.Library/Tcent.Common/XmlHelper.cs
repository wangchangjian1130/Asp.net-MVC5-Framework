using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tcent.Common
{
    public class XmlHelper
    {
        #region { 01.通用的XML操作方法 }

        /// <summary>
        /// 生成XmlReader
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <param name="ignorecomments">忽略注释</param>
        /// <returns>XmlReader</returns>
        public static XmlReader CreateReader(string filename, bool ignorecomments = true)
        {
            var settings = new XmlReaderSettings();
            settings.IgnoreComments = ignorecomments;
            return XmlReader.Create(filename, settings);
        }

        /// <summary>
        /// 将xml字符串转换成数据集
        /// </summary>
        /// <param name="filename">xml文件路径</param>
        /// <returns>数据集</returns>
        public static DataSet ReadXml2Set(string filename)
        {
            var ds = new DataSet();
            if (System.IO.File.Exists(filename))
            {
                ds.ReadXml(filename);
            }
            return ds;
        }

        #endregion
    }
}
