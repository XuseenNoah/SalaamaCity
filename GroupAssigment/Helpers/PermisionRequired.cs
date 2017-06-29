using GroupAssigment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GroupAssigment.Helpers
{
    public class PermisionRequired:ActionFilterAttribute
    {
        LoginForm.Permissions expectedPermissions;
        //LoginForm.Permissions ex;

        public PermisionRequired(LoginForm.Permissions permissions = LoginForm.Permissions.None)
        {
            this.expectedPermissions = permissions;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            ActionResult unauthorizedActionResult = new ViewResult { ViewName = "Unauthorized" };
            ActionResult logoutActionResult = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { controller = "Account", action = "Logout", returnUrl = filterContext.HttpContext.Request.RawUrl }));

            var user = filterContext.HttpContext.Session["User"] as LoginForm;

            if (user == null)
            {
                filterContext.Result = logoutActionResult;
                return;
            }

            //var userCurrentPermissions = user.CurrentPermissions;
            var userCurrentPermissions = (LoginForm.Permissions)HttpContext.Current.Cache[string.Format("{0}'s CurrentPermision", user.Username)];

            if (((userCurrentPermissions & expectedPermissions) != expectedPermissions))
                filterContext.Result = unauthorizedActionResult;
        }
    }
}