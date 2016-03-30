using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Tcent.IBase;

namespace Tcent.Api.Web.ApiControllers
{
    /// <summary>
    /// 基础数据Api控制器
    /// </summary>
    [RoutePrefix("api/Base")]
    public class BaseController : InitApiController
    {
        /// <summary>
        /// 基础模块接口
        /// </summary>
        [Dependency]
        public IBaseDao _IBaseDao { get; set; }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }

        [HttpGet]
        [Route("GetList")]
        public List<string> GetList()
        {
            var list = _IBaseDao.GetList();
            return list;
        }
    }
}