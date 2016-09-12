using System.Web;

namespace AuthishModule
{
    public class AuthishHandler : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            var rawUrl = context.Request.RawUrl;
            if (ValidationService.PasswordIsCorrect(context.Request.Params["password"]))
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
                                       "<div style='margin: 0 auto; width: 300px; padding-top: 300px'>" +
                (ValidationService.IsAuthishPasswordMissing()
                    ? "<div style='color: red'>Password not set in appsettings - please contact administrator</div>"
                    : "<form method='post' action='" + rawUrl + "'>" +
                        "<input name='username' style='display: none' />" +
                        "<input type='password' name='password' autofocus " + fontSize + "/><input type='submit' value='Log in'" + fontSize + "/>" +
                      "</form>" +
                    "") +
                "</div></body></html>");
            }
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
