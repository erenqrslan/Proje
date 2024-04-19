using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasoyon.Models.Siniflar;
using Newtonsoft.Json.Linq;

namespace MvcOnlineTicariOtomasoyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index()
        {
            var urunler = c.Uruns.Where(x => x.Durum == true).ToList();//Linq Where(x=>x.Durum==true) sorgusu ile sadece durumu true olanları listeledik
            return View(urunler);
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriİD.ToString()
                                           }).ToList();
            //Bu yapılan işlemler DropdownList işlemleri içindir
            //Kullanıcıya gosterilecek olan kısım yani burda textti
            //secilen ogeye gore arka planda calısacak yapı yani burda İD
            //deger1 view kısmına tasımak için ViewBag kullanılır
            //ViewBag controller tarafından view tarafına veri veya değer tasımak için kullanılır
            //ViewBag nasıl tanımlanır Viewbag. yazdıktan sonra ViewBagin ismin yazıyoruz; ViewBag.dgr1 daha sonra
            //dgr1 viewbaginin değerinin nereden geleceğini yazıyoruz ViewBag.dgr1=deger1; şeklinde
            ViewBag.dgr1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun p)
        {
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        //Urun Silme işlemi(Urunu tamamen silmiyoruz cunku ilişkili bir tablo,sorun olabilir. Silmek yerine durumunu false yaptık)
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        //Urun Güncelleme Sayfasına Ürünleri Getirme İşlemi
        public ActionResult UrunGetir(int id) 
        {
            //dropdown menu hata verdiği için bu kısmada ViewBag eklenmiştir
            List<SelectListItem> deger1 = (from x in c.Kategoris.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.KategoriAd,
                                               Value = x.KategoriİD.ToString()
                                           }).ToList();
            ViewBag.dgr1 = deger1;
            var urundeger = c.Uruns.Find(id);
            return View("UrunGetir", urundeger);
        }
        //Urun güncelleme işlemei
        public ActionResult UrunGuncelle(Urun p) 
        {
            var urn = c.Uruns.Find(p.Urunid);
            urn.AlisFiyat = p.AlisFiyat;
            urn.Durum = p.Durum;
            urn.KategoriİD = p.KategoriİD;
            urn.Marka = p.Marka;
            urn.SatisFiyat = p.SatisFiyat;
            urn.Stok = p.Stok;
            urn.UrunAd = p.UrunAd;
            urn.UrunGorsel = p.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }   
    }
}