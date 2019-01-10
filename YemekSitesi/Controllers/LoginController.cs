using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using YemekSitesi.Ayarlar;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class LoginController : Controller
    {
        private  YemekContext db = new YemekContext();
        public ActionResult Index()
        {
            if (Session["Kullanici"]!=null)
            {
                return RedirectToAction("Index", "Home");
            } 
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string eposta, string sifre)
        {
            //Sifrelenmis sifreyi sifrelenmemis sifre ile kontrol ettik
            Kullanici k = db.Kullanici.Where(x => x.eposta == eposta ).SingleOrDefault();
            Boolean a = Crypto.VerifyHashedPassword(k.sifre, sifre);
            if (k == null || Crypto.VerifyHashedPassword(k.sifre, sifre)==false)
            {
               
                ViewBag.Sonuc = "Kullanici Adi ve Sifreye uyusan kayıt bulunamadı";
               
                return View();
            }
            else if(k.aktifMi==false)
            {
                Session.Abandon();
                return View();
            }
            else
            {
                Session["Kullanici"] = k;
                //Kullanıcı bulundu;
                return RedirectToAction("Index", "Home");
            }
        }
        public ActionResult SifremiUnuttum()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SifremiUnuttum(string eposta)
        {
            try
            {
                Kullanici k = db.Kullanici.Where(x => x.eposta == eposta).SingleOrDefault();
                if (k == null)
                {
                    ViewBag.Sonuc = " Epostaya Ait Kişi bulunamadi";
                }
                else
                {
                    String sifre = Crypto.GenerateSalt();//Benzersiz sifre olusturdu
                    k.sifre = Crypto.HashPassword(sifre); // sifreleyip veri tabanina koydu
                    db.SaveChanges();
                    string mesaj = "";
                    mesaj = mesaj + "<b>" + k.ad + " " + k.soyad.ToUpper() + "</b> <br/>";
                    mesaj = mesaj + "<b> Yeni sifreniz: <h1>" + sifre + "<h1>  </b> <br/><br/><hr/>";

                    Eposta.Gonder("Sifre Yenileme Talebi", mesaj, eposta);
                    TempData["uyari"] = "Sifreniz basarı ile sıfırlandı";
                }
            }
            catch (Exception)
            {
                ViewBag.Sonuc="bilinmeyen bir hata olustu";
                return View();
            }
            return View();
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