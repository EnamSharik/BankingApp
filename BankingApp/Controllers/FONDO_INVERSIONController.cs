using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BankingApp.Models;

namespace BankingApp.Controllers
{
    public class FONDO_INVERSIONController : Controller
    {
        private Entities db = new Entities();

        // GET: FONDO_INVERSION
        public ActionResult Index()
        {
            var fONDO_INVERSION = db.FONDO_INVERSION.Include(f => f.MONEDA);
            return View(fONDO_INVERSION.ToList());
        }

        // GET: FONDO_INVERSION/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FONDO_INVERSION fONDO_INVERSION = db.FONDO_INVERSION.Find(id);
            if (fONDO_INVERSION == null)
            {
                return HttpNotFound();
            }
            return View(fONDO_INVERSION);
        }

        // GET: FONDO_INVERSION/Create
        public ActionResult Create()
        {
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO");
            return View();
        }

        // POST: FONDO_INVERSION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_FONDO,ID_MONEDA,FECHA,CAPITAL_INVERSIONISTAS,CAPITAL_PRESTADO,CAPITAL_DISPONIBLE,FECHA_CREACION")] FONDO_INVERSION fONDO_INVERSION)
        {
            if (ModelState.IsValid)
            {
                db.FONDO_INVERSION.Add(fONDO_INVERSION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", fONDO_INVERSION.ID_MONEDA);
            return View(fONDO_INVERSION);
        }

        // GET: FONDO_INVERSION/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FONDO_INVERSION fONDO_INVERSION = db.FONDO_INVERSION.Find(id);
            if (fONDO_INVERSION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", fONDO_INVERSION.ID_MONEDA);
            return View(fONDO_INVERSION);
        }

        // POST: FONDO_INVERSION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_FONDO,ID_MONEDA,FECHA,CAPITAL_INVERSIONISTAS,CAPITAL_PRESTADO,CAPITAL_DISPONIBLE,FECHA_CREACION")] FONDO_INVERSION fONDO_INVERSION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fONDO_INVERSION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", fONDO_INVERSION.ID_MONEDA);
            return View(fONDO_INVERSION);
        }

        // GET: FONDO_INVERSION/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FONDO_INVERSION fONDO_INVERSION = db.FONDO_INVERSION.Find(id);
            if (fONDO_INVERSION == null)
            {
                return HttpNotFound();
            }
            return View(fONDO_INVERSION);
        }

        // POST: FONDO_INVERSION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            FONDO_INVERSION fONDO_INVERSION = db.FONDO_INVERSION.Find(id);
            db.FONDO_INVERSION.Remove(fONDO_INVERSION);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
