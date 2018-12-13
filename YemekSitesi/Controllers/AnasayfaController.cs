using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class AnasayfaController : Controller
    {
        private YemekContext db = new YemekContext();
        // GET: Anasayfa
        public ActionResult Home()
        {
            ViewBag.Blog = db.Blog.Take(3).ToList();
            return View(db.Yemek.Take(6).ToList());
        }
        public ActionResult TarifListele()
        {
            ViewBag.Kategori = db.Kategori.ToList();
            return View(db.Yemek.Take(12).ToList());
        }
        public ActionResult Tarif(int id)
        {
            Yemek y = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
            return View(y);
        }
        public ActionResult Bloglar()
        {
            return View(db.Blog.Take(12).ToList());
        }
        public ActionResult Blog(int id)
        {
            Blog blog = db.Blog.Where(x=>x.blogID==id).SingleOrDefault();
            ViewBag.Kategori = db.Kategori.ToList();
            return View(blog);
        }
        public ActionResult Bul()
        {
 
            return View();
        }

        public PartialViewResult Kategoriler()
        {
            return PartialView("_KategoriListesi", @db.Kategori.ToList());
        }
        public PartialViewResult KullaniciLar()
        {
            return PartialView("_Kullanicilar", @db.Kullanici.Take(9).ToList());
        }
        public PartialViewResult resimsizKategoriler()
        {
            return PartialView("_KategoriResimsizListele", @db.Kategori.ToList());
        }
    }
}