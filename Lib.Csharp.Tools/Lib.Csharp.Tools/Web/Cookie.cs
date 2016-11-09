using System;
using System.Web;

namespace Lib.Csharp.Tools.Web
{
    public sealed class Cookie
    {
        public static string Get(string name)
        {
            try
            {
                var cookie = HttpContext.Current.Request.Cookies.Get(name);
                if (cookie == null)
                {
                    return string.Empty;
                }
                return cookie.Value;
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
            return string.Empty;
        }

        public static string GetSessionId(string sessionKey)
        {
            return Get(sessionKey);
        }

        public static void Remove(string name)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = "",
                    Expires = DateTime.Now.AddYears(-100),
                    HttpOnly = false
                };
                HttpContext.Current.Request.Cookies.Add(cookie);
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Clear()
        {
            try
            {
                HttpContext.Current.Response.Cookies.Clear();
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Set(string cookieDomain,string name, string value, bool httpOnly = false)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = value,
                    HttpOnly = httpOnly
                };
                if (!string.IsNullOrWhiteSpace(cookieDomain))
                {
                    cookie.Domain = cookieDomain;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void Set(string cookieDomain, string name, string value, int expireDays, bool httpOnly = false)
        {
            try
            {
                var cookie = new HttpCookie(name)
                {
                    Value = value,
                    HttpOnly = httpOnly,
                    Expires = DateTime.Now.AddDays(expireDays)
                };
                if (!string.IsNullOrWhiteSpace(cookieDomain))
                {
                    cookie.Domain = cookieDomain;
                }
                HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch (Exception e)
            {
                Log.Error(e);
            }
        }

        public static void SetSessionId(string cookieDomain, string sessionKey, string sessionId)
        {
            Set(cookieDomain,sessionKey, sessionId);
        }
    }
}