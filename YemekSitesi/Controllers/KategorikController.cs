using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Ayarlar;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class KategorikController : Controller
    {
        private YemekContext db = new YemekContext();
        // Kategori
        public ActionResult KategoriListele()
        {

            return View(db.Kategori.ToList());
        }
        public ActionResult KategoriEkle()
        {

            return View(new Kategori());
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori ka, HttpPostedFileBase resimGelen)
        {
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                return View();
            }
            if (resimGelen == null)
            {
                ka.resim = "bos.png";
            }
            else
            {
                string yeniResimAdi = "";
                ResimIslemleri r = new ResimIslemleri();
                yeniResimAdi = r.Ekle(resimGelen,"Kategoriler");
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
                    ka.resim = yeniResimAdi;
                }
            }

            db.Kategori.Add(ka);
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriDuzenle(int id)
        {
            TempData["KategoriID"] = id;
            Kategori k = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
            return View(k);
        }
        [HttpPost]
        public ActionResult KategoriDuzenle(Kategori k, HttpPostedFileBase resimGelen)
        {
            int id = (int)TempData["KategoriID"];
            Kategori ka = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {

                return View();
            }
            if (resimGelen != null)
            {
                ResimIslemleri r = new ResimIslemleri();
                string deger = r.Ekle(resimGelen, "Kategoriler");

                if (deger == "uzanti")
                {

                    ViewBag.Hata = "Lütfen .png veya .jpg uzantılı dosya giriniz.";
                    return View(ka);
                }
                else if (deger == "boyut")
                {
                    ViewBag.Hata = "Lütfen daha düşük boyutlu bir resim giriniz.";
                    return View(ka);
                }
                else
                {
                    k.resim = deger;
                }
            }
            if (ka.resim != null)
            {
                // yeni resim başarılı eklendiyse
                if (ka.resim != "bos.png")
                {
                    // eski resmi sil
                    new ResimIslemleri().Sil(ka.resim,"Kategoriler");
                }

                // yeni resmi at
                ka.resim = k.resim;
            }

            ka.kategoriAdi = k.kategoriAdi;
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriSil(int id)
        {
            Kategori k = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
            ResimIslemleri r = new ResimIslemleri();
            r.Sil(k.resim, "Kategoriler");
            db.Kategori.Remove(k);
            db.SaveChanges();
            return RedirectToAction("KategoriListele");
        }

        //Ulke
        public ActionResult UlkeListele()
        {

            return View(db.Ulkeler.ToList());
        }
        public ActionResult UlkeEkle()
        {

            return View(new Ulkeler());
        }
        [HttpPost]
        public ActionResult UlkeEkle(Ulkeler u )
        {
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                return View();
            }
            db.Ulkeler.Add(u);
            db.SaveChanges();
            return RedirectToAction("UlkeListele");
        }
        public ActionResult UlkeDuzenle(int id)
        {
            TempData["UlkeiID"] = id;
            Ulkeler u = db.Ulkeler.Where(x => x.ulkeID == id).SingleOrDefault();
            return View(u);
        }
        [HttpPost]
        public ActionResult UlkeDuzenle(Ulkeler u)
        {
            int id = (int)TempData["UlkeiID"];
            Ulkeler ul = db.Ulkeler.Where(x => x.ulkeID == id).SingleOrDefault();
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {

                return View();
            }
            ul.ulkeAd = u.ulkeAd;
            db.SaveChanges();
            return RedirectToAction("UlkeListele");
        }
        public ActionResult UlkeSil(int id)
        {
            Ulkeler u = db.Ulkeler.Where(x => x.ulkeID == id).SingleOrDefault();
            db.Ulkeler.Remove(u);
            db.SaveChanges();
            return RedirectToAction("UlkeListele");
        }

        //zorluk derecesi
        public ActionResult ZorlukListele()
        {

            return View(db.ZorlukDerecesi.ToList());
        }
        public ActionResult ZorlukEkle()
        {

            return View(new ZorlukDerecesi());
        }
        [HttpPost]
        public ActionResult ZorlukEkle(ZorlukDerecesi z)
        {
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {
                return View();
            }
            db.ZorlukDerecesi.Add(z);
            db.SaveChanges();
            return RedirectToAction("ZorlukListele");
        }
        public ActionResult ZorlukDuzenle(int id)
        {
            TempData["ZorlukiID"] = id;
            ZorlukDerecesi z = db.ZorlukDerecesi.Where(x => x.zorlukDerecesiID == id).SingleOrDefault();
            return View(z);
        }
        [HttpPost]
        public ActionResult ZorlukDuzenle(ZorlukDerecesi z)
        {
            int id = (int)TempData["ZorlukiID"];
            ZorlukDerecesi zd = db.ZorlukDerecesi.Where(x => x.zorlukDerecesiID == id).SingleOrDefault();
            if (ModelState.IsValid == false) // validation false gelirse hata var
            {

                return View();
            }
            zd.zorlukTanımı = z.zorlukTanımı;
            db.SaveChanges();
            return RedirectToAction("ZorlukListele");
        }
        public ActionResult ZorlukSil(int id)
        {
            ZorlukDerecesi z = db.ZorlukDerecesi.Where(x => x.zorlukDerecesiID == id).SingleOrDefault();
            db.ZorlukDerecesi.Remove(z);
            db.SaveChanges();
            return RedirectToAction("ZorlukListele");
        }
    }
}