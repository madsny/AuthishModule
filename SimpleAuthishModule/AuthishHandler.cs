using System.Collections.Specialized;
using System.Configuration;
using System.Web;
using log4net;

namespace SimpleAuthishModule
{
    public class AuthishHandler : IHttpHandler
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AuthishHandler));
        private static readonly string AuthishPassword = ConfigurationManager.AppSettings["AuthishPassword"];

        public void ProcessRequest(HttpContext context)
        {
            var rawUrl = context.Request.RawUrl;
            if (PasswordIsCorrect(context.Request.Params))
            {
                SessionHelper.SetAuthenticated(context);
                context.Response.Redirect(rawUrl, false);
            }
            else
            {
                const string fontSize = " style='font-size: 32px'";
                context.Response.Write("<!DOCTYPE html PUBLIC \" -//W3C//DTD XHTML 1.0 Transitional//EN\" \"http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd\">" +
                                       "<html><head>" +
                                       "<head/><body>" +
                                       "<div style='margin: 0 auto; width: 300px; padding-top: 300px'>"
                + "<form method='post' action='" + rawUrl + "'>"
                + "<input type='password' name='password' autofocus " + fontSize + "/><input type='submit' value='Logg inn'" + fontSize + "/>" +
                "</form>" +
                (string.IsNullOrEmpty(AuthishPassword)
                    ? "<div style='color: red'>Password not set in appsettings - please contact administrator</div>" : "") +
                "</div></body></html>");
            }
        }

        private bool PasswordIsCorrect(NameValueCollection parameters)
        {
            var password = parameters["password"];
            var passwordIsCorrect = !string.IsNullOrEmpty(password) && password == AuthishPassword;
            if (!passwordIsCorrect)
            {
                Log.Info(string.Format("Authish logon failed, user password: '{0}', authish wants: '{1}", password, AuthishPassword));
            }

            return passwordIsCorrect;
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
