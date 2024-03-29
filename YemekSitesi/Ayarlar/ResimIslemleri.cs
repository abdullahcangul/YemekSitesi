﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace YemekSitesi.Ayarlar
{
    public class ResimIslemleri
    {
        public string Ekle(HttpPostedFileBase orjResim,String yer)
        {
            string uzanti = Path.GetExtension(orjResim.FileName);
            if (!(uzanti == ".jpg" || uzanti == ".png"))
            {
                return "uzanti";
            }

            if (orjResim.ContentLength > 100000000)
            {
                return "boyut";
            }

            string ad = Guid.NewGuid().ToString() + uzanti;
            Bitmap res = new Bitmap(Image.FromStream(orjResim.InputStream));
            res.Save(HttpContext.Current.Server.MapPath("/Content/Resimler/"+yer+"/" + ad));
            return ad;
        }

        public string Sil(string resimAdi,String yer)
        {
            string yol = HttpContext.Current.Server.MapPath("/Content/Resimler/"+ yer +"/" + resimAdi);
            if (System.IO.File.Exists(yol)) // belirtilen kalasörde o dosya var mı
            {
                System.IO.File.Delete(yol); // eski resmi sil
            }
            else
            {
                return "Bulunamadı";
            }
            return "Silindi";
        }
    }
}