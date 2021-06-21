using System;
using System.Web;
using System.Web.SessionState;

namespace AuthishModule
{
    public class BlockingModule : IHttpModule
    {

        public void Init(HttpApplication context)
        {
            context.BeginRequest +=
                (s, e) => context.Context.Response.Cache
                    .AddValidationCallback(ShouldBypassCacheValidator, null);
            context.PreRequestHandlerExecute += context_PostAcquireRequestState;
        }

        private static void ShouldBypassCacheValidator(
            HttpContext context, object data,
            ref HttpValidationStatus validationstatus)
        {
            validationstatus = ShouldAuthenticate(context) 
                ? HttpValidationStatus.IgnoreThisRequest // Bypass cache => authenticate
                : HttpValidationStatus.Valid;
        }

        private void context_PostAcquireRequestState(object sender, EventArgs e)
        {
            HttpApplication app = (HttpApplication)sender;
            if(app.Context.Handler is IRequiresSessionState && ShouldAuthenticate(app.Context))
            {
                app.Context.Handler = new AuthishHandler();
            }
        }

        private static bool ShouldAuthenticate(HttpContext context)
        {
            return (context.Session == null || !SessionHelper.IsAuthenticated(context.Session)) &&
                   !ValidationService.PasswordIsCorrect(context.Request.Headers["Authish"]) &&
                   !ValidationService.PathIsWhitelisted(context.Request.Path) &&
                   !context.Request.IsLocal;
        }

        public void Dispose()
        {
            
        }
    }
}
