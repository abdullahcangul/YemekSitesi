using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using YemekSitesi.Models;

namespace YemekSitesi
{
    public class HomeModel
    {

        public int kullaniciSayisi { get; set; }
        public int blogSayisi { get; set; }
        public int yorumSayisi { get; set; }
        public int yemekSayisi { get; set; }
    }
}