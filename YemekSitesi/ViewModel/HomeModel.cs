using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSitesi.Models;

namespace YemekSitesi.ViewModel
{
    public class HomeModel
    {

        public int kullaniciSayisi { get; set; }
        public int blogSayisi { get; set; }
        public int yorumSayisi { get; set; }
        public int YemekSayisi { get; set; }
        public Kullanici kisi { get; set; }
    }
}