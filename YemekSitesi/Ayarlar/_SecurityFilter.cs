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
            Kullanici k = (Kullanici)HttpContext.Current.Session["Kullanici"];
            if ((k == null || k.aktifMi == false) && ControllerName != "Anasayfa" && ControllerName != "Login")
            {
                filterContext.Result = new RedirectResult("/Anasayfa/Home");
                return;
            }

            if (HttpContext.Current.Session["Kullanici"] != null)
            {
                Kullanici ku = (Kullanici)HttpContext.Current.Session["Kullanici"];
                if (ku.adminMi == true && ku.aktifMi==true) { }
                else if ((ControllerName == "Kategorik" || ControllerName == "Yonetim" || ControllerName == "Yorum") && ControllerName != "Anasayfa")
                {

                    filterContext.Result = new RedirectResult("/Home/Index");
                    return;
                }
            }
            base.OnActionExecuting(filterContext);
        }
    }
}