﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Ayarlar;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class BlogController : Controller
    {
        private YemekContext db = new YemekContext();
        // GET: Blog
        public ActionResult BlogListele()
        {
            return View(db.Blog.OrderByDescending(x=>x.tarih).ToList());
        }
        public ActionResult BlogEkle()
        {
            ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
            return View(new Blog());
        }
        [HttpPost]
        public ActionResult BlogEkle(Blog blog,HttpPostedFileBase resimGelen)
        {
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                return View();
            }
            if (resimGelen == null)
            {
                blog.resim = "bos.png";
            }
            else
            {
                string yeniResimAdi = "";
                ResimIslemleri r = new ResimIslemleri();
                yeniResimAdi = r.Ekle(resimGelen,"Bloglar");
                //yeniResimAdi = new ResimIslem().Ekle(resimGelen);

                if (yeniResimAdi == "uzanti")
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewData["Hata"] = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                    return View();
                }
                else if (yeniResimAdi == "boyut")
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewData["Hata"] = "En fazla 1MB boyutunda dosya girebilirsiniz.";
                    return View();
                }
                else
                {
                    blog.resim = yeniResimAdi;
                }
            }
            blog.tarih = DateTime.Now;
            db.Blog.Add(blog);
            db.SaveChanges();
            return RedirectToAction("BlogListele");
        }
        public ActionResult BlogDuzenle(int id)
        {
            TempData["BlogID"] = id;
            Blog b = db.Blog.Where(x => x.blogID == id).SingleOrDefault();
            ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
            return View(b);
        }
        [HttpPost]
        public ActionResult BlogDuzenle(Blog blog,HttpPostedFileBase resimGelen)
        {
            int id= (int)TempData["BlogID"] ;
            Blog b = db.Blog.Where(x => x.blogID == id).SingleOrDefault();
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                return View();
            }
            if (resimGelen != null)
            {
                ResimIslemleri r = new ResimIslemleri();
                string deger = r.Ekle(resimGelen, "Bloglar");

                if (deger == "uzanti")
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewBag.Hata = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                    return View(blog);
                }
                else if (deger == "boyut")
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewBag.Hata = "Lütfen daha düşük boyutlu bir resim giriniz.";
                    return View(blog);
                }
                else
                {
                    b.resim = deger;
                }
            }
            if (blog.resim != null)
            {
                // yeni resim başarılı eklendiyse
                if (blog.resim != "bos.png")
                {
                    // eski resmi sil
                    new ResimIslemleri().Sil(blog.resim,"Bloglar");
                }

                // yeni resmi at
                b.resim = blog.resim;
            }

            b.baslik = blog.baslik;
            b.icerik = blog.icerik;
            b.KategoriID = blog.KategoriID;
            db.SaveChanges();
            return RedirectToAction("BlogListele");
        }
        public ActionResult BlogSil(int id)
        {
            Blog b = db.Blog.Where(x => x.blogID == id).SingleOrDefault();
            ResimIslemleri r = new ResimIslemleri();
            r.Sil(b.resim, "Bloglar");
            db.Blog.Remove(b);
            db.SaveChanges();
            return RedirectToAction("BlogListele");
        }
    }
}