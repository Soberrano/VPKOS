using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.ServiceModel.Channels;

namespace WebApplication2.Models
{

    /// <summary>
    /// Фильтр по состоянию авторизации, который не редиректит на страницу авторизации
    /// </summary>
    public class NotRedirectWebApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            IIdentity identity = null;
            IPrincipal principal = actionContext.Request.GetUserPrincipal();
            if (principal != null) identity = principal.Identity;

            if (identity == null || !identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
        }
    }
}