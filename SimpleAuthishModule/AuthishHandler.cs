using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace SimpleAuthishModule
{
    public class AuthishHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            if(PasswordIsCorrect(context.Request.Params))
            {
                SessionHelper.SetAuthenticated(context);
                context.Response.Redirect(context.Request.RawUrl, false);    
            }
            else
            {
                context.Response.Write("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\"><html><head><head/><body><div style='margin: 0 auto; width: 300px; padding-top: 300px;'>" 
                +"<form method='post' action='" + context.Request.RawUrl + "'>"
                + "<input type='password' name='password' /><input type='submit' value='Logg inn' /> </form></div></body></html>");
            }
            
            
        }

        private bool PasswordIsCorrect(NameValueCollection parameters)
        {
            return parameters["password"] != null && parameters["password"] == ConfigurationManager.AppSettings["AuthishPassword"];

        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
