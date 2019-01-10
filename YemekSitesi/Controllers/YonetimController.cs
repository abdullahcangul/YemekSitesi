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
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciEkle(Kullanici k,HttpPostedFileBase resimGelen)
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
                k.aktifMi = true;
                k.sifre = Crypto.HashPassword(k.sifre);//Sifre sifrelendi
                db.Kullanici.Add(k);
                db.SaveChanges();
                TempData["uyari"] = k.ad+" isimli kullanici basrı ile eklenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Kullanıcı eklerken hata olustu";
                return RedirectToAction("KullaniciListele");
            }
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
        [ValidateAntiForgeryToken]
        public ActionResult KullaniciDuzenle(Kullanici k, HttpPostedFileBase resimGelen)
        {
            try
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
                    string deger = r.Ekle(resimGelen, "Kullanicilar");

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
                if (k.resim != null)
                {
                    // yeni resim başarılı eklendiyse
                    if (ku.resim != "bos.png")
                    {
                        // eski resmi sil
                        new ResimIslemleri().Sil(ku.resim, "Kullanicilar");
                    }

                    // yeni resmi at
                    ku.resim = k.resim;
                }
                if (k.sifre != null)
                {
                    ku.sifre = Crypto.HashPassword(k.sifre);
                }
                ku.ad = k.ad;
                ku.eposta = k.eposta;
                ku.meslegi = k.meslegi;
                ku.soyad = k.soyad;
                db.SaveChanges();
                TempData["uyari"] = k.ad + " isimli kullanici basrı ile duzenlenmiştir";
            }
            catch (Exception e)
            {
                TempData["tehlikeli"] = "Kullanıcı duzenlerken hata olustu";
                return RedirectToAction("KullaniciListele");
            }
          
            return RedirectToAction("KullaniciListele");
        }
        public ActionResult KullaniciSil(int id)
        {
            try
            {
                Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
                if (db.Yemek.Where(x => x.kullaniciID == k.kullaniciID).Count()==0 && db.Blog.Where(x => x.kullanıcıID == k.kullaniciID).Count()==0)
                {
                    ResimIslemleri r = new ResimIslemleri();
                    r.Sil(k.resim, "Kullanicilar");
                    string ad = k.ad;
                    db.Kullanici.Remove(k);
                    db.SaveChanges();
                    TempData["uyari"] = ad + " isimli kullanici basrı ile silinmiştir";
                }
                else
                {
                    TempData["tehlikeli"] = "Kullanıcı'ya ait yemek veya blog oldugu için silinememiştir.";
                }
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Kullanıcı silerken hata olustu";
                return RedirectToAction("KullaniciListele");
            }
       
            return RedirectToAction("KullaniciListele");
        }
        //admin ve aktif pasif yapma
        public ActionResult AktifEt(int id)
        {
            
            if (db.Kullanici.Where(x=>x.aktifMi==true).Where(x=>x.aktifMi==true).Count()>0)
            {
                Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
                k.adminMi = Convert.ToBoolean(k.adminMi) ? false : true;
                if (k.adminMi==true)
                {
                    TempData["uyari"] = "Kullanıcı admin oldu";
                }
                else
                {
                    TempData["uyari"] = "Admin Kullanıcı oldu";
                }
                db.SaveChanges();
            }
            else
            {
                TempData["hata"] = "En az bir tane admin olmali";
            }
            return Redirect("/Yonetim/KullaniciDetay/" + id);
        }
        public ActionResult AktifEt2(int id)
        {
            
            if (db.Kullanici.Where(x => x.aktifMi == true).Where(x => x.aktifMi == true).Count() > 0)
            {
                Kullanici k = db.Kullanici.Where(x => x.kullaniciID == id).SingleOrDefault();
                k.aktifMi = Convert.ToBoolean(k.aktifMi) ? false : true;
                if (k.aktifMi == true)
                {
                    TempData["uyari"] = "Kullanıcı Aktif oldu";
                }
                else
                {
                    TempData["uyari"] = "Kullanici Pasif oldu";
                }
                db.SaveChanges();
            }
            else
            {
                TempData["hata"] = "En az bir tane admin olmali";
            }
            
            return Redirect("/Yonetim/KullaniciDetay/" + id);
        }
    }
}