﻿using System;
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
            hm.yemekSayisi = db.Yemek.Count();
            hm.blogSayisi = db.Blog.Count();
            hm.kullaniciSayisi = db.Kullanici.Count();
            hm.yorumSayisi = db.Yorum.Count();
            ViewBag.sayi = hm;
            return View(db.Yemek.Take(6).ToList());
        }
        //q ile button gizleme yapıldı
        public ActionResult TarifListele(int? id, string q)
        {
            ViewBag.Kategori = db.Kategori.ToList();
            List<Yemek> y;
            if (q == "gizle")
            {
                y = db.Yemek.ToList();
                ViewBag.denetle = "aa";
            }
            else
            {
                y = db.Yemek.Take(12).ToList();
            }
            if (id != null)
            {
                if (db.Yemek.Where(x => x.kategoriID == id).Count() <= 0)
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
            TempData["yemekId"] = id;
            return View(y);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tarif(Yorum y)
        {
            int id =(int) TempData["yemekId"];
            Yemek ye=db.Yemek.Where(x => x.yemekID == id).SingleOrDefault(); ;
            try
            {
                ye = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
                y.yemekID = ye.yemekID;
                y.onaylimi = false;
                y.tarih = DateTime.Now;
                db.Yorum.Add(y);
                db.SaveChanges();
            }
            catch
            {

                return View(ye);
            }
            return View(ye);
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
                //Bloglar içindeki buttonları gizler id kategoriden gelir
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
            Blog blog = db.Blog.Where(x => x.blogID == id).SingleOrDefault();
            ViewBag.Kategori = db.Kategori.ToList();
            TempData["blogId"] = id;
            return View(blog);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Blog(Yorum y)
        {
            int id = (int)TempData["blogId"];
            Blog ye = db.Blog.Where(x => x.blogID == id).SingleOrDefault();
            try
            {
                y.blogID = ye.blogID;
                y.onaylimi = false;
                y.tarih = DateTime.Now;
                db.Yorum.Add(y);
                db.SaveChanges();
                TempData["uyari"] =" yorum basarı ile eklendi";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Yorum gonderirken hata olustu";
                return View(ye);
            }
   
            return View(ye);
        }
        public ActionResult Iletisim()
        {
            return View(new Iletisim());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Iletisim(Iletisim i)
        {
            try
            {
                if (ModelState.IsValid == false)
                {
                    ViewBag.Sonuc = "Gecersiz islem";
                    return View();
                }
                else
                {
                    i.tarih = DateTime.Now;
                    i.telefon = i.telefon.ToString();
                    db.Iletisim.Add(i);
                    db.SaveChanges();
                    TempData["uyari"] = "Mesajınız alınmıştır";
                    return View();

                }
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Hata olustu";
                return View();
                
            }
           
        }



        public ActionResult Arama(int? id)
        {
            ViewBag.kategoriler = db.Kategori.ToList();
            if (id != null)
            {
                return View(db.Yemek.Where(x => x.Kategori.kategoriID == id).ToList());
            }

            return View(db.Yemek.ToList());
        }
        [HttpPost]
        public ActionResult Arama(string ad)
        {
            ViewBag.kategoriler = db.Kategori.ToList();
            return View(db.Yemek.Where(x => x.ad.Contains(ad)).ToList());
        }
        public ActionResult KayıtOl()
        {
            return View(new Kullanici());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult KayıtOl(Kullanici k, HttpPostedFileBase resimGelen)
        {
            try
            {
                Kullanici kul = db.Kullanici.Where(x => x.eposta == k.eposta).SingleOrDefault();
                if (kul != null)
                {
                    ViewData["Hata"] = "Kayıtlı bir eposta kullandınız!!!";
                    return View();
                }
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {
                    ViewData["Hata"] = "";
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

                k.adminMi = false;
                k.aktifMi = false;
                db.Kullanici.Add(k);
                db.SaveChanges();
                TempData["uyari"] = "Kayıt işleminiz alınmıştır";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Hata olustu";
                return View();
            }
            
            return View();
        }
        public ActionResult Giris()
        {
            return View();
        }
        public ActionResult hataSayfasi()
        {
            return View();
        }
        public PartialViewResult Kategoriler()
        {
            return PartialView("_KategoriListesi", @db.Kategori.ToList());
        }
        public ActionResult KullaniciArama(int id)
        {
            ViewBag.kategoriler = db.Kategori.ToList();
            List<Yemek> k = db.Yemek.Where(x => x.kullaniciID == id).ToList();
            return View("Arama", k);
        }
        public PartialViewResult KullaniciLar()
        {
            return PartialView("_Kullanicilar", @db.Kullanici.Take(9).ToList());
        }
        public PartialViewResult resimsizKategoriler()
        {
            return PartialView("_KategoriResimsizListele", @db.Kategori.ToList());
        }

        public PartialViewResult resimsizKategorilerBlog()
        {
            return PartialView("__KategoriBlogResimsizListele", @db.Kategori.ToList());
        }
    }
}