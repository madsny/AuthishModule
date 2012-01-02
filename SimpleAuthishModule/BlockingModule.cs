using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.SessionState;

namespace SimpleAuthishModule
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
            if(app.Context.Handler is IRequiresSessionState && !SessionHelper.IsAuthenticated(app.Session))
            {
                app.Context.Handler = new AuthishHandler();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
