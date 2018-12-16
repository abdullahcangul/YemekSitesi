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
            if (HttpContext.Current.Session["Kullanici"] == null && ControllerName != "Anasayfa"&& ControllerName!="Login")
            {
                filterContext.Result = new RedirectResult("/Login/Index");
                return;
            }
            if (HttpContext.Current.Session["Kullanici"] != null)
            {
                Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];
                if (k.adminMi != true && ( (ControllerName == "Kategorik" || ControllerName == "Yonetim" || ControllerName == "Yorum" )&& ControllerName != "Anasayfa"))
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}