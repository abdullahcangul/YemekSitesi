using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Ayarlar;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class YonetimController : Controller
    {
        private YemekContext db = new YemekContext();
        // GET: Yonetim
        public ActionResult KullaniciListele()
        {
            return View(db.Kullanici.ToList());
        }
        public ActionResult KullaniciEkle()
        {
            return View(new Kullanici());
        }
        [HttpPost]
        public ActionResult KullaniciEkle(Kullanici k,HttpPostedFileBase resimGelen)
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
                yeniResimAdi = r.Ekle(resimGelen,"Kullanicilar");
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
            return RedirectToAction("KullaniciListele");
        }
        public ActionResult KullaniciDetay(int id)
        {
            Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            return View(k);
        }

        public ActionResult KullaniciDuzenle(int id)
        {
            TempData["KullaniciID"] = id;
            Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            return View(k);
        }
        [HttpPost]
        public ActionResult KullaniciDuzenle(Kullanici k, HttpPostedFileBase resimGelen)
        {
            int id = (int)TempData["KullaniciID"];
            Kullanici ku = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                return View();
            }
            if (resimGelen != null)
            {
                ResimIslemleri r = new ResimIslemleri();
                string deger = r.Ekle(resimGelen,"Kullanicilar");

                if (deger == "uzanti")
                {
                    ViewBag.Hata = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                    return View(ku);
                }
                else if (deger == "boyut")
                {
                    ViewBag.Hata = "Lütfen daha düşük boyutlu bir resim giriniz.";
                    return View(ku);
                }
                else
                {
                    k.resim = deger;
                }
            }
            if (ku.resim != null)
            {
                // yeni resim başarılı eklendiyse
                if (ku.resim != "bos.png")
                {
                    // eski resmi sil
                    new ResimIslemleri().Sil(ku.resim,"Kullanicilar");
                }

                // yeni resmi at
                ku.resim = k.resim;
            }

            ku.ad = k.ad;
            ku.eposta = k.eposta;
            ku.sifre = k.sifre;
            ku.soyad = k.soyad;
            db.SaveChanges();
            return RedirectToAction("KullaniciListele");
        }
        public ActionResult KullaniciSil(int id)
        {
            Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            ResimIslemleri r = new ResimIslemleri();
            r.Sil(k.resim,"Kullanicilar");
            db.Kullanici.Remove(k);
            db.SaveChanges();
            return RedirectToAction("KullaniciListele");
        }
        //admin ve aktif pasif yapma
        public ActionResult AktifEt(int id)
        {
            Kullanici k=db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            k.adminMi=Convert.ToBoolean(k.adminMi)? false : true;
            db.SaveChanges();
            return Redirect("/Yonetim/KullaniciDetay/" + k.kullaniciID);
        }
        public ActionResult AktifEt2(int id)
        {
            Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
            k.aktifMi = Convert.ToBoolean(k.aktifMi) ? false : true;
            db.SaveChanges();
            return Redirect("/Yonetim/KullaniciDetay/" + k.kullaniciID);
        }
    }
}