using personelTakipMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace personelTakipMVC.Controllers
{
    public class DepartmanController : Controller
    {
        // GET: Departman
        PersonelDbEntities db = new PersonelDbEntities();

        public ActionResult Index()
        {
            var model = db.Departman.ToList();
            return View(model);
        }
        [HttpGet] // Veriyi okuma öz nitelik
        public ActionResult Yeni()
        {
            return View("DepartmanForm");
        }

        [HttpPost]

        public ActionResult Kaydet(Departman departman)
        {
            if (departman.Id == 0) // id 0 ise yeni kayıt
            {
                db.Departman.Add(departman);
            }
            else // id si olan kaydı güncelle
            {
                var guncellenecekdepartman = db.Departman.Find(departman.Id);
                if (guncellenecekdepartman == null)
                {
                    return HttpNotFound();
                }
                guncellenecekdepartman.Ad = departman.Ad;
            }
            db.SaveChanges();
            return RedirectToAction("Index","Departman");
        }
        public ActionResult Guncelle(int id)
        {

            var model = db.Departman.Find(id);
            if (model == null)
            {
                return HttpNotFound();
            }
            return View("DepartmanForm",model);
        }
        public ActionResult Sil(int id)
        {
            var silinicekdepartman = db.Departman.Find(id);
            if (silinicekdepartman == null)
                return HttpNotFound();
            db.Departman.Remove(silinicekdepartman);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



    }
}