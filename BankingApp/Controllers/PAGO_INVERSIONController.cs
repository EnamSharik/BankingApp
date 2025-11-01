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
    public class PAGO_INVERSIONController : Controller
    {
        private Entities db = new Entities();

        // GET: PAGO_INVERSION
        public ActionResult Index()
        {
            var pAGO_INVERSION = db.PAGO_INVERSION.Include(p => p.INVERSION);
            return View(pAGO_INVERSION.ToList());
        }

        // GET: PAGO_INVERSION/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_INVERSION pAGO_INVERSION = db.PAGO_INVERSION.Find(id);
            if (pAGO_INVERSION == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_INVERSION);
        }

        // GET: PAGO_INVERSION/Create
        public ActionResult Create()
        {
            var inversionesConClientes = db.INVERSION
                .Include(i => i.CLIENTE) 
                .ToList(); 

            var listaInversionesConNombre = inversionesConClientes
                .Select(i => new
                {
                    ID_VALOR = i.ID_INVERSION,

            TEXTO_DESPLEGADO = i.ID_INVERSION.ToString() + " - " + i.CLIENTE.NOMBRE
                })
                .ToList();

            ViewBag.ID_INVERSION = new SelectList(
                listaInversionesConNombre,
                "ID_VALOR",
                "TEXTO_DESPLEGADO"
            );
            return View();
        }

        // POST: PAGO_INVERSION/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PAGO_INVERSION,ID_INVERSION,NUMERO_PAGO,FECHA_PAGO_PROGRAMADA,MONTO_INTERES,FECHA_PAGO_REAL,ESTADO,FECHA_CREACION")] PAGO_INVERSION pAGO_INVERSION)
        {
            if (ModelState.IsValid)
            {
                db.PAGO_INVERSION.Add(pAGO_INVERSION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_INVERSION = new SelectList(db.INVERSION, "ID_INVERSION", "MODALIDAD_PAGO", pAGO_INVERSION.ID_INVERSION);
            return View(pAGO_INVERSION);
        }

        // GET: PAGO_INVERSION/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_INVERSION pAGO_INVERSION = db.PAGO_INVERSION.Find(id);
            if (pAGO_INVERSION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_INVERSION = new SelectList(db.INVERSION, "ID_INVERSION", "MODALIDAD_PAGO", pAGO_INVERSION.ID_INVERSION);
            return View(pAGO_INVERSION);
        }

        // POST: PAGO_INVERSION/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PAGO_INVERSION,ID_INVERSION,NUMERO_PAGO,FECHA_PAGO_PROGRAMADA,MONTO_INTERES,FECHA_PAGO_REAL,ESTADO,FECHA_CREACION")] PAGO_INVERSION pAGO_INVERSION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pAGO_INVERSION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_INVERSION = new SelectList(db.INVERSION, "ID_INVERSION", "MODALIDAD_PAGO", pAGO_INVERSION.ID_INVERSION);
            return View(pAGO_INVERSION);
        }

        // GET: PAGO_INVERSION/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PAGO_INVERSION pAGO_INVERSION = db.PAGO_INVERSION.Find(id);
            if (pAGO_INVERSION == null)
            {
                return HttpNotFound();
            }
            return View(pAGO_INVERSION);
        }

        // POST: PAGO_INVERSION/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PAGO_INVERSION pAGO_INVERSION = db.PAGO_INVERSION.Find(id);
            db.PAGO_INVERSION.Remove(pAGO_INVERSION);
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
