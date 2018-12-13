using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Ayarlar;
using YemekSitesi.Models;
using YemekSitesi.ViewModel;

namespace YemekSitesi.Controllers
{
    public class AnasayfaController : Controller
    {
        private YemekContext db = new YemekContext();
        // GET: Anasayfa
        public ActionResult Home()
        {
            ViewBag.Blog = db.Blog.Take(3).ToList();
            HomeModel hm = new HomeModel();
             hm.yemekSayisi= db.Yemek.Count();
            hm.blogSayisi = db.Blog.Count();
            hm.kullaniciSayisi = db.Kullanici.Count();
            hm.yorumSayisi = db.Yorum.Count();
            ViewBag.sayi = hm;
            return View(db.Yemek.Take(6).ToList());
        }
        //q ile button gizleme yapıldı
        public ActionResult TarifListele(int? id,string q)
        {
            ViewBag.Kategori = db.Kategori.ToList();
            List<Yemek> y;
            if (q=="gizle")
            {
                 y = db.Yemek.ToList();
                ViewBag.denetle = "aa";
            }
            else
            {
                y = db.Yemek.Take(12).ToList();
            }
            if (id!=null)
            {
                if(db.Yemek.Where(x => x.kategoriID == id).Count() <= 0)
                {
                    ViewBag.denetle = "aabb";
                }
                return View(db.Yemek.Where(x => x.kategoriID == id).ToList());
            }
            
            return View(y);
        }
        public ActionResult Tarif(int id)
        {
            Yemek y = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
            return View(y);
        }
        public ActionResult Bloglar(int? id, string q)
        {
            List<Blog> b;
            if (q == "gizle")
            {
                b = db.Blog.ToList();
                ViewBag.denetle = "aa";
            }
            else
            {
                b = db.Blog.Take(12).ToList();
            }
            if (id != null)
            {
                if (db.Blog.Where(x => x.KategoriID == id).Count() <= 0)
                {
                    ViewBag.denetle = "aabb";
                }
                return View(db.Blog.Where(x => x.KategoriID == id).ToList());
            }

            return View(b);
        }
        public ActionResult Blog(int id)
        {
            Blog blog = db.Blog.Where(x=>x.blogID==id).SingleOrDefault();
            ViewBag.Kategori = db.Kategori.ToList();
            return View(blog);
        }
        public ActionResult Iletisim()
        {
            return View();
        }
        
        public ActionResult KayıtOl()
        {
            return View(new Kullanici());
        }
        public ActionResult Bul()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KayıtOl(Kullanici k, HttpPostedFileBase resimGelen)
        {
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {

                return View();
            }
            if (resimGelen == null)
            {
                k.resim = "bos.png";
            }
            else
            {
                string yeniResimAdi = "";
                ResimIslemleri r = new ResimIslemleri();
                yeniResimAdi = r.Ekle(resimGelen, "Kullanicilar");
                //yeniResimAdi = new ResimIslem().Ekle(resimGelen);

                if (yeniResimAdi == "uzanti")
                {
                    ViewData["Hata"] = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                    return View();
                }
                else if (yeniResimAdi == "boyut")
                {
                    ViewData["Hata"] = "En fazla 1MB boyutunda dosya girebilirsiniz.";
                    return View();
                }
                else
                {
                    k.resim = yeniResimAdi;
                }
            }
            db.Kullanici.Add(k);
            db.SaveChanges();
            return RedirectToAction("Home");
        }
        public ActionResult Giris()
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