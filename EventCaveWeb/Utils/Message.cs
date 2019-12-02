using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EventCaveWeb.Utils
{
    public class Message
    {
        public static void Create(HttpResponseBase response, string message)
        {
            response.Cookies.Add(new HttpCookie("message", message));
        }

        public static void Clear(HttpResponseBase response)
        {
            response.Cookies["message"].Expires = DateTime.Now.AddMonths(-1);
        }

        public static bool Exists(HttpRequestBase request)
        {
            return request.Cookies["message"] != null;
        }

        public static string Get(HttpRequestBase request)
        {
            return request.Cookies["message"].Value;
        }
        
    }
}