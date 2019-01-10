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
            try
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
                    yeniResimAdi = r.Ekle(resimGelen, "Kategoriler");
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

                TempData["uyari"] = ka.kategoriAdi + " isimli kategori Basarı ile eklenmiştir";

            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Kategori Eklenirken Hata olustu";
                return RedirectToAction("KategoriListele");
            }

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
            try
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
                if (k.resim != null)
                {
                    // yeni resim başarılı eklendiyse
                    if (ka.resim != "bos.png")
                    {
                        // eski resmi sil
                        new ResimIslemleri().Sil(ka.resim, "Kategoriler");
                    }

                    // yeni resmi at
                    ka.resim = k.resim;
                }
              
                ka.kategoriAdi = k.kategoriAdi;
                db.SaveChanges();

                TempData["uyari"] = ka.kategoriAdi + " isimli kategori Basarı ile duzenlenmiştir";

            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Kategori düzenlerken Hata olustu";
                return RedirectToAction("KategoriListele");
            }
            return RedirectToAction("KategoriListele");
        }
        public ActionResult KategoriSil(int id)
        {
            try
            {
                Kategori k = db.Kategori.Where(x => x.kategoriID == id).SingleOrDefault();
                if (k.Yemek.Count==0 && k.Blog.Count == 0)
                {
                    ResimIslemleri r = new ResimIslemleri();
                    r.Sil(k.resim, "Kategoriler");
                    db.Kategori.Remove(k);
                    db.SaveChanges();
                    TempData["uyari"] = "Kategori basarı ile silindi";
                }
                else
                {
                    TempData["tehlikeli"] = "Kategori ait yemek veya blog oldugundan silinemedi.Lütfen önce gerekli ögeleri silin";
                }

            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Kategori silerken Hata olustu";
                return RedirectToAction("KategoriListele");
            }

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
        public ActionResult UlkeEkle(Ulkeler u)
        {
            try
            {
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {
                    return View();
                }
                db.Ulkeler.Add(u);
                db.SaveChanges();
                TempData["uyari"] = u.ulkeAd + " isimli ulke Basarı ile eklenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "ulke eklenirken hata olustu";
                return RedirectToAction("UlkeListele");
            }

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
            try
            {
                int id = (int)TempData["UlkeiID"];
                Ulkeler ul = db.Ulkeler.Where(x => x.ulkeID == id).SingleOrDefault();
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {

                    return View();
                }
                ul.ulkeAd = u.ulkeAd;
                db.SaveChanges();
                TempData["uyari"] = u.ulkeAd + " isimli ulke Basarı ile duzenlenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "ulke duzenlenirken hata olustu";
                return RedirectToAction("UlkeListele");
            }

            return RedirectToAction("UlkeListele");
        }
        public ActionResult UlkeSil(int id)
        {
            try
            {
                Ulkeler u = db.Ulkeler.Where(x => x.ulkeID == id).SingleOrDefault();
                if (u.Yemek.Count == 0)
                {
                    db.Ulkeler.Remove(u);
                    db.SaveChanges();
                    TempData["uyari"] = "ulke basarı ile silindi ";
                }
                else
                {
                    TempData["tehlikeli"] = "Ulkeye ait yemek oldugundan silinemedi.Lütfen önce yemegi silin";
                }

            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "ulke silerken hata olustu";
                return RedirectToAction("UlkeListele");
            }

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

            try
            {
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {
                    return View();
                }
                db.ZorlukDerecesi.Add(z);
                db.SaveChanges();
                TempData["uyari"] = z.zorlukTanımı + " isimli zorluk Basarı ile eklenmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Zorluk eklenirken  hata olustu";
                return RedirectToAction("ZorlukListele");
            }
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


            try
            {
                int id = (int)TempData["ZorlukiID"];
                ZorlukDerecesi zd = db.ZorlukDerecesi.Where(x => x.zorlukDerecesiID == id).SingleOrDefault();
                if (ModelState.IsValid == false) // validation false gelirse hata var
                {

                    return View();
                }
                zd.zorlukTanımı = z.zorlukTanımı;
                db.SaveChanges();
                TempData["uyari"] = z.zorlukTanımı + " isimli zorluk Basarı ile duzenlenmiştir";
            }
            catch (Exception)
            {

                TempData["tehlikeli"] = "Zorluk duzenlenirken  Hata olustu";
                return RedirectToAction("ZorlukListele");
            }
            return RedirectToAction("ZorlukListele");
        }
        public ActionResult ZorlukSil(int id)
        {
            try
            {
                ZorlukDerecesi z = db.ZorlukDerecesi.Where(x => x.zorlukDerecesiID == id).SingleOrDefault();
                if (z.Yemek.Count == 0)
                {

                    db.ZorlukDerecesi.Remove(z);
                    db.SaveChanges();
                    TempData["uyari"] = "zorluk basarı ile silinmiştir";
                }
                else
                {
                    TempData["tehlikeli"] = "Zorluk Derecesine ait yemek oldugundan silinemedi.Lütfen önce yemegi silin";
                }

            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Zorluk silerken hata olusutu";
                return RedirectToAction("ZorlukListele");
            }

            return RedirectToAction("ZorlukListele");
        }

        public ActionResult IletisimListele()
        {
            return View(db.Iletisim.ToList());
        }
        public ActionResult IletisimDetay(int id)
        {
            TempData["Iletisimid"] = id;
            return View(db.Iletisim.Where(x => x.ID == id).SingleOrDefault());
        }
        [HttpPost]
        public ActionResult IletisimDetay(string icerik)
        {
            try
            {
                int id = (int)TempData["Iletisimid"];
                Iletisim i = db.Iletisim.Where(x => x.ID == id).SingleOrDefault();

                string mesaj = "";
                mesaj = mesaj + "<b>Sayın " + i.isim + "</b> <br/>";
                mesaj = mesaj + icerik + "<hr/>";
                mesaj = mesaj + i.icerik;
                Eposta.Gonder("İletisim isteginiz hakkında", mesaj, i.eposta);
                TempData["uyari"] = " Basarı ile yanıtlanmıştır";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Yanıtlarken hata oldu";
                return RedirectToAction("IletisimListele");
            }
            return RedirectToAction("IletisimListele");
        }


        public ActionResult IletisimSil(int id)
        {
            try
            {
                Iletisim i = db.Iletisim.Where(x => x.ID == id).SingleOrDefault();

                db.Iletisim.Remove(i);
                db.SaveChanges();
                TempData["uyari"] = "Basarı ile silinmiştir";
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "Hata olustu";
                return RedirectToAction("IletisimListele");
            }
            return RedirectToAction("IletisimListele");
        }

    }
}