using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace YazilimYonetimAraci.Helper
{
    public class CustomFilter:FilterAttribute,  IActionFilter
    {
        //Action metod tetiklendiği anda devreye girer.
        public void OnActionExecuting(ActionExecutingContext session)
        {

            HttpContextWrapper wrapper = new HttpContextWrapper(HttpContext.Current);

            if (session.HttpContext.Session["UserID"]== null || String.IsNullOrEmpty(session.HttpContext.Session["UserID"].ToString()))
            {
                session.Result = new RedirectToRouteResult(
                new RouteValueDictionary { { "controller", "User" }, { "action", "Login" } });
            }

        }

        //Action metod çalıştırıldıktan sonra devreye girer.
        public void OnActionExecuted(ActionExecutedContext session)
        {

        }
    }
}