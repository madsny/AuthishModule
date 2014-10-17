using System;
using System.Web;
using System.Web.SessionState;

namespace AuthishModule
{
    public class BlockingModule : IHttpModule
    {

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += context_PostAcquireRequestState;
        }

        private void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;

            if(app.Context.Handler is IRequiresSessionState &&
                !SessionHelper.IsAuthenticated(app.Session) &&
                !ValidationService.PasswordIsCorrect(app.Context.Request.Headers["Authish"]))
            {
                app.Context.Handler = new AuthishHandler();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
