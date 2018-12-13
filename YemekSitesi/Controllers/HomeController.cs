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
            Kullanici kisi = db.Kullanici.Where(x => x.kullaniciID == 1).SingleOrDefault();//sesindan kullanici al
            List<Kullanici> k = db.Kullanici.OrderByDescending(x=>x.Yemek.Count).ToList();
            List<Blog> b = db.Blog.ToList();
            List<Yorum> y = db.Yorum.ToList();
            List<Yemek> ye = db.Yemek.ToList();
            hm.kullaniciSayisi=k.Count();
            hm.blogSayisi =b.Count();
            hm.yorumSayisi = y.Count() + y.Count();
            hm.yemekSayisi = ye.Count();
            //hm.kisi=kisi;
            ViewBag.kullanici = k;
            return View(hm);
        }
      
    }
}