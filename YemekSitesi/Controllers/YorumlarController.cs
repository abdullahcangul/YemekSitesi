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
            return View(db.Yorum.ToList());
        }
        public ActionResult YorumDetay(int id)
        {
            Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
            return View(y);
        }
        public ActionResult YorumSil(int id)
        {
            
            Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
            if (y.YorumCevap==null)
            {
                TempData["sil"] = "İlk önce Yorum Detayları siliniz";
                return View(y);
            }
            db.Yorum.Remove(y);
            db.SaveChanges();
            return Redirect("/Yorumlar/YorumListele/" + id);
        }
        public ActionResult YorumOnay(int id)
        {
            Yorum y = db.Yorum.Where(x => x.yorumID == id).SingleOrDefault();
            if (y.onaylimi==true)
            {
                y.onaylimi = false;
            }
            else
            {
                y.onaylimi = true;
            }
            db.SaveChanges();
            return Redirect("/Yorumlar/YorumDetay/"+id);
        }
        public ActionResult YorumCevapSil(int id)
        {
            YorumCevap y = db.YorumCevap.Where(x => x.YorumCevapID == id).SingleOrDefault();
            db.YorumCevap.Remove(y);
            db.SaveChanges();
            return Redirect("/Yorumlar/YorumListele/" + id);
        }
        
        public ActionResult YorumCevapDetay(int id)
        {
            YorumCevap y = db.YorumCevap.Where(x => x.YorumCevapID == id).SingleOrDefault();
            return View(y);
        }


    }
}