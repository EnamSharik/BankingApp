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
    public class PAGO_PRESTAMOController : Controller
    {
        private Entities db = new Entities();

        // GET: PAGO_PRESTAMO
        public ActionResult Index()
        {
            var pAGO_PRESTAMO = db.PAGO_PRESTAMO.Include(p => p.PRESTAMO);
            return View(pAGO_PRESTAMO.ToList());
        }

        // GET: PAGO_PRESTAMO/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_PRESTAMO pAGO_PRESTAMO = db.PAGO_PRESTAMO.Find(id);
            if (pAGO_PRESTAMO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_PRESTAMO);
        }

        // GET: PAGO_PRESTAMO/Create
        public ActionResult Create()
        {
            ViewBag.ID_PRESTAMO = new SelectList(db.PRESTAMO, "ID_PRESTAMO", "MODALIDAD_PAGO");
            return View();
        }

        // POST: PAGO_PRESTAMO/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PAGO_PRESTAMO,ID_PRESTAMO,NUMERO_PAGO,FECHA_PAGO_PROGRAMADA,MONTO_INTERES,FECHA_PAGO_REAL,ESTADO,FECHA_CREACION")] PAGO_PRESTAMO pAGO_PRESTAMO)
        {
            if (ModelState.IsValid)
            {
                db.PAGO_PRESTAMO.Add(pAGO_PRESTAMO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_PRESTAMO = new SelectList(db.PRESTAMO, "ID_PRESTAMO", "MODALIDAD_PAGO", pAGO_PRESTAMO.ID_PRESTAMO);
            return View(pAGO_PRESTAMO);
        }

        // GET: PAGO_PRESTAMO/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_PRESTAMO pAGO_PRESTAMO = db.PAGO_PRESTAMO.Find(id);
            if (pAGO_PRESTAMO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_PRESTAMO = new SelectList(db.PRESTAMO, "ID_PRESTAMO", "MODALIDAD_PAGO", pAGO_PRESTAMO.ID_PRESTAMO);
            return View(pAGO_PRESTAMO);
        }

        // POST: PAGO_PRESTAMO/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PAGO_PRESTAMO,ID_PRESTAMO,NUMERO_PAGO,FECHA_PAGO_PROGRAMADA,MONTO_INTERES,FECHA_PAGO_REAL,ESTADO,FECHA_CREACION")] PAGO_PRESTAMO pAGO_PRESTAMO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGO_PRESTAMO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_PRESTAMO = new SelectList(db.PRESTAMO, "ID_PRESTAMO", "MODALIDAD_PAGO", pAGO_PRESTAMO.ID_PRESTAMO);
            return View(pAGO_PRESTAMO);
        }

        // GET: PAGO_PRESTAMO/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_PRESTAMO pAGO_PRESTAMO = db.PAGO_PRESTAMO.Find(id);
            if (pAGO_PRESTAMO == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_PRESTAMO);
        }

        // POST: PAGO_PRESTAMO/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PAGO_PRESTAMO pAGO_PRESTAMO = db.PAGO_PRESTAMO.Find(id);
            db.PAGO_PRESTAMO.Remove(pAGO_PRESTAMO);
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
