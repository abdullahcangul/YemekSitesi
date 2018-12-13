using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Models;

namespace YemekSitesi.Ayarlar
{
    public class _SecurityFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string ControllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            if (HttpContext.Current.Session["Kullanici"] == null && ControllerName != "Login")
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (HttpContext.Current.Session["Kullanici"] != null)
            {
                Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];

            }
            base.OnActionExecuting(filterContext);
        }
    }
}