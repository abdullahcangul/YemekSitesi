using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Models;
using YemekSitesi.ViewModel;

namespace YemekSitesi.Content
{
    public class HomeController : Controller
    {
        YemekContext db = new YemekContext();
        // GET: Home
        public ActionResult Index()
        {
            HomeModel hm = new HomeModel();
            Kullanici kisi =(Kullanici) Session["Kullanici"];
            List<Kullanici> k = db.Kullanici.OrderByDescending(x=>x.Yemek.Count).Take(5).ToList();
            hm.kullaniciSayisi= db.Kullanici.Count();
            hm.blogSayisi = db.Blog.Count();
            hm.yorumSayisi = db.Yorum.Count();
            hm.yemekSayisi = db.Yemek.Count();
            ViewBag.kullanici = k;
            ViewBag.kisi = kisi;
            return View(hm);
        }
      
    }
}