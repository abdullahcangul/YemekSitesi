using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class LoginController : Controller
    {
        private  YemekContext db = new YemekContext();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string eposta, string sifre)
        {
            Kullanici k = db.Kullanici.Where(x => x.eposta == eposta && x.sifre == sifre).SingleOrDefault();
            if (k == null)
            {
               
                ViewBag.Sonuc = "Kullanıcı bulunamadı";
                return View();
            }
            else
            {
                Session["Kullanici"] = k;
                //Kullanıcı bulundu;
                return RedirectToAction("Index", "Home");
            }
        }
        
        public PartialViewResult KullaniciResim()
        {
            Kullanici k =(Kullanici)Session["Kullanici"];
            return PartialView("PartialViews/_KullaniciResim", k);
        }
        public PartialViewResult KisiyeGoreErisim()
        {
            Kullanici k = (Kullanici)Session["Kullanici"];
            return PartialView("PartialViews/_ErisimListesi", k);
        }
        public ActionResult Cikis()
        {
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}