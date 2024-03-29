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
    public class YemekController : Controller
    {
        private YemekContext db = new YemekContext();
        // GET: Yemek
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult YemekListele()
        {
            //Admin sie tüm yemekleri gor degil ise sadece kendi yeeklerini gorsun
            Kullanici k = (Kullanici)Session["Kullanici"];
            if (k.adminMi==true)
            {
                return View(db.Yemek.OrderByDescending(x => x.tarih).ToList());
            }
            return View(db.Yemek.Where(x => x.kullaniciID == k.kullaniciID).OrderByDescending(x => x.tarih).ToList());
        }
        public ActionResult YemekEkle()
        {
            ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
            ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
            ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
            return View(new Yemek());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YemekEkle(Yemek y, HttpPostedFileBase resimGelen)
        {
            try
            {
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                    ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                    return View();
                }
                if (resimGelen == null)
                {
                    y.resim = "bos.png";
                }
                else
                {
                    string yeniResimAdi = "";
                    ResimIslemleri r = new ResimIslemleri();
                    yeniResimAdi = r.Ekle(resimGelen, "Yemekler");
                    //yeniResimAdi = new ResimIslem().Ekle(resimGelen);

                    if (yeniResimAdi == "uzanti")
                    {
                        ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                        ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                        ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                        ViewData["Hata"] = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                        return View();
                    }
                    else if (yeniResimAdi == "boyut")
                    {
                        ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                        ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                        ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                        ViewData["Hata"] = "En fazla 1MB boyutunda dosya girebilirsiniz.";
                        return View();
                    }
                    else
                    {
                        y.resim = yeniResimAdi;
                    }
                }
                y.tarih = DateTime.Now;
                Kullanici k = (Kullanici)Session["Kullanici"];
                y.kullaniciID = k.kullaniciID;
                db.Yemek.Add(y);
                db.SaveChanges();
                TempData["uyari"] = y.ad + " isimli yemek eklenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Yemek eklerken bir hata olustu";
                return RedirectToAction("YemekListele");
            }
            
            return RedirectToAction("YemekListele");
        }
        public ActionResult YemekDuzenle(int id)
        {
            TempData["KullaniciID"] = id;
            Yemek y=db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
            ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
            ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
            ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
            return View(y);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult YemekDuzenle(Yemek y, HttpPostedFileBase resimGelen)
        {
            try
            {
                int id = (int)TempData["KullaniciID"];
                Yemek ye = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {
                    ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                    ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                    ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                    return View();
                }
                if (resimGelen != null)
                {
                    ResimIslemleri r = new ResimIslemleri();
                    string deger = r.Ekle(resimGelen, "Yemekler");

                    if (deger == "uzanti")
                    {

                        ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                        ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                        ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                        ViewBag.Hata = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                        return View(ye);
                    }
                    else if (deger == "boyut")
                    {
                        ViewBag.kategori = new SelectList(db.Kategori.ToList(), "kategoriID", "kategoriAdi");
                        ViewBag.zorlukDerecesi = new SelectList(db.ZorlukDerecesi.ToList(), "zorlukDerecesiID", "zorlukTanımı");
                        ViewBag.ulke = new SelectList(db.Ulkeler.ToList(), "ulkeID", "ulkeAd");
                        ViewBag.Hata = "Lütfen daha düşük boyutlu bir resim giriniz.";
                        return View(ye);
                    }
                    else
                    {
                        y.resim = deger;
                    }
                }
                if (y.resim != null)
                {
                    // yeni resim başarılı eklendiyse
                    if (ye.resim != "bos.png")
                    {
                        // eski resmi sil
                        new ResimIslemleri().Sil(ye.resim, "Yemekler");
                    }

                    // yeni resmi at
                    ye.resim = y.resim;
                }

                ye.ad = y.ad;
                ye.kacKisilik = y.kacKisilik;
                ye.pisirmeSuresi = y.pisirmeSuresi;
                ye.kategoriID = y.kategoriID;
                ye.aciklama = y.aciklama;
                ye.ulkeID = y.ulkeID;
                ye.zorlukDerecesiID = y.zorlukDerecesiID;
                db.SaveChanges();
                TempData["uyari"] = y.ad + " isimli yemek basarı ile duzenlenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Yemek eklerken bir hata olustu";
                return RedirectToAction("YemekListele");
            }
            return RedirectToAction("YemekListele");
            
        }
        public ActionResult YemekDetay(int? id)
        {
            Yemek ym = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
            return View(ym);
        }
        public ActionResult YemekSil(int id)
        {
            try
            {
                Yemek y = db.Yemek.Where(x => x.yemekID == id).SingleOrDefault();
                ResimIslemleri r = new ResimIslemleri();
                string ad = y.ad;
                db.Mazeme.RemoveRange(db.Mazeme.Where(x => x.yemekID == id));
                db.Tarif.RemoveRange(db.Tarif.Where(x => x.yemekID == id));
                db.BesinDegerleri.RemoveRange(db.BesinDegerleri.Where(x => x.yemekID == id));
                db.Yorum.RemoveRange(db.Yorum.Where(x => x.yemekID == id));
                r.Sil(y.resim, "Yemekler");
                db.Yemek.Remove(db.Yemek.Where(x => x.yemekID == id).SingleOrDefault());
                db.SaveChanges();
                TempData["uyari"] = ad + " isimli yemek ve bilesenleri basarı ile silinmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Yemek silerken bir hata olustu";
                return RedirectToAction("YemekListele");
            }

            return RedirectToAction("YemekListele");
        }

        //Tarif işlemleri
        public ActionResult TarifEkle(int id)
        {
            Tarif t = new Tarif();
            t.siraNo = (db.Tarif.Where(x => x.yemekID == id).Count()+1);
            t.yemekID = id;
            TempData["YemekID"] = id;
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]//güvenlik amaclı kondu
        public ActionResult TarifEkle(Tarif t)
        {
            t.yemekID =(int)TempData["YemekID"];
            db.Tarif.Add(t);
            db.SaveChanges();
            List<Tarif> tarifler = db.Tarif.Where(x => x.yemekID == t.yemekID).ToList();
            int sayac = 1;
            foreach (Tarif item in tarifler)
            {
                item.siraNo = sayac;
                db.SaveChanges();
                sayac++;
            }
            return Redirect("/Yemek/TarifEkle/" + t.yemekID);
        }


        public ActionResult TarifDuzenle(int id)
        {
            TempData["tarifID"] = id;
            Tarif t = db.Tarif.Where(x => x.tarifID == id).SingleOrDefault();
            TempData["YemekID"] =t.yemekID;
            return View(t);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult TarifDuzenle(Tarif t)
        {
            t.tarifID = (int)TempData["tarifID"];
            Tarif ta = db.Tarif.Where(x => x.tarifID == t.tarifID).SingleOrDefault();
            ta.aciklama = t.aciklama;
            int id = (int)TempData["YemekID"];
            db.SaveChanges();
            List<Tarif> tarifler = db.Tarif.Where(x=>x.yemekID==id).ToList();
            int sayac = 1;
            foreach (Tarif item in tarifler)
            {
                item.siraNo = sayac;
                db.SaveChanges();
                sayac++;
            }
            return Redirect("/Yemek/YemekDetay/" + id );
        }
        public ActionResult TarifSil(int? id)
        {
            Tarif t = db.Tarif.Where(x => x.tarifID == id).SingleOrDefault();
            int Id=(int)t.yemekID;
            db.Tarif.Remove(t);
            db.SaveChanges();
            List<Tarif> tarifler = db.Tarif.Where(x => x.yemekID == Id).ToList();
            int sayac = 1;
            foreach (Tarif item in tarifler)
            {
                item.siraNo = sayac;
                db.SaveChanges();
                sayac++;
            }
            return Redirect("/Yemek/YemekDetay/" + Id);
        }


        //mazeme işlemleri
        public ActionResult MazemeEkle(int id)
        {
            TempData["YemekID"] = id;
            Mazeme m = new Mazeme();
            m.yemekID = id;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MazemeEkle(Mazeme m)
        {
            m.yemekID = (int)TempData["YemekID"];
            db.Mazeme.Add(m);
            db.SaveChanges();
            return Redirect("/Yemek/MazemeEkle/" + m.yemekID);
        }


        public ActionResult MazemeDuzenle(int id)
        {
            TempData["MazemeID"] = id;
            Mazeme m = db.Mazeme.Where(x => x.mazemeID == id).SingleOrDefault();
            TempData["YemekID"] = m.yemekID;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MazemeDuzenle(Mazeme m)
        {
            m.mazemeID = (int)TempData["MazemeID"];
            Mazeme ma = db.Mazeme.Where(x => x.mazemeID == m.mazemeID).SingleOrDefault();
            ma.mazemeAdi = m.mazemeAdi;
            ma.miktar = m.miktar;
            ma.birim = m.birim;
            int id = (int)TempData["YemekID"];
            db.SaveChanges();
            return Redirect("/Yemek/YemekDetay/" + id);
        }
        public ActionResult MazemeSil(int? id)
        {
            Mazeme m = db.Mazeme.Where(x => x.mazemeID == id).SingleOrDefault();
            int Id = (int)m.yemekID;
            db.Mazeme.Remove(m);
            db.SaveChanges();
            return Redirect("/Yemek/YemekDetay/" + Id);
        }
        //Besin Deger işlemleri
        public ActionResult BesinDegerleriEkle(int id)
        {
            TempData["YemekID"] = id;
            BesinDegerleri m = new BesinDegerleri();
            m.yemekID = id;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BesinDegerleriEkle(BesinDegerleri m)
        {
            m.yemekID = (int)TempData["YemekID"];
            db.BesinDegerleri.Add(m);
            db.SaveChanges();
            return Redirect("/Yemek/BesinDegerleriEkle/" + m.yemekID);
        }
        public ActionResult BesinDegerleriDuzenle(int id)
        {
            TempData["BesinDegerleriID"] = id;
            BesinDegerleri m = db.BesinDegerleri.Where(x => x.besinDegerID == id).SingleOrDefault();
            TempData["YemekID"] = m.yemekID;
            return View(m);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BesinDegerleriDuzenle(BesinDegerleri m)
        {
            m.besinDegerID = (int)TempData["BesinDegerleriID"];
            BesinDegerleri ma = db.BesinDegerleri.Where(x => x.besinDegerID == m.besinDegerID).SingleOrDefault();
            ma.besinAdi = m.besinAdi;
            ma.deger = m.deger;
            int id = (int)TempData["YemekID"];
            db.SaveChanges();
            return Redirect("/Yemek/YemekDetay/" + id);
        }
        public ActionResult BesinDegerleriSil(int? id)
        {
            BesinDegerleri m = db.BesinDegerleri.Where(x => x.besinDegerID == id).SingleOrDefault();
            int Id = (int)m.yemekID;
            db.BesinDegerleri.Remove(m);
            db.SaveChanges();
            return Redirect("/Yemek/YemekDetay/" + Id);
        }


    }
}