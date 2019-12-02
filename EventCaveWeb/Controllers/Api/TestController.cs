using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace EventCaveWeb.Controllers.Api
{
    [RoutePrefix("Api/Test")]
    public class TestController : ApiController
    {
        [Route("")]
        public object Get()
        {
            return new { firstName = "LMAO", lastName = "topKek", array = new string[] { "value1", "value2" } };
        }

        [Route("{id:int}")]
        public string Get(int id)
        {
            return id.ToString();
        }
    }
}