using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YemekSitesi.Models;

namespace YemekSitesi.Controllers
{
    public class YorumlarController : Controller
    {
        private YemekContext db = new YemekContext();
       
        // GET: Yorum
        public ActionResult YorumListele()
        {
           Kullanici k=(Kullanici) Session["Kullanici"];
            if (k.adminMi==true)
            {//admin herseyi görebilir
                return View(db.Yorum.ToList());
            }
            else
            {//Yemek e veya blog a göre yazara özel yorum getirme
                return View(db.Yorum.Where(x => x.Yemek.kullaniciID == k.kullaniciID||x.Blog.kullanıcıID==k.kullaniciID).ToList());
            }
        }
        public ActionResult YorumDetay(int id)
        {
            Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
            return View(y);
        }
        public ActionResult YorumSil(int id)
        {
            try
            {
                Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
                db.Yorum.Remove(y);
                db.SaveChanges();
                TempData["uyari"] = "Yorum basarı ile silindi";
            }
            catch (Exception)
            {

                TempData["tehlikeli"] = "hata olustu";
                return Redirect("/Yorumlar/YorumListele/" + id);
            }
            
            return Redirect("/Yorumlar/YorumListele/" + id);
        }
        public ActionResult YorumOnay(int id)
        {
            try
            {
                Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
                if (y.onaylimi == true)
                {
                    y.onaylimi = false;
                    TempData["uyari"] = "Yorum onay kaldırılmıştır";
                }
                else
                {
                    y.onaylimi = true;
                    TempData["uyari"] = "Yorum Onaylanmıştır";
                }
                db.SaveChanges();
                
            }
            catch (Exception)
            {
                TempData["tehlikeli"] = "hata olustu";
                return Redirect("/Yorumlar/YorumListele/");
            }
          
            return Redirect("/Yorumlar/YorumListele/");
        }
    }
}