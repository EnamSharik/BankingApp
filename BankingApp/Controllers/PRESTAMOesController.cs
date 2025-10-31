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
    public class PRESTAMOesController : Controller
    {
        private Entities db = new Entities();

        // GET: PRESTAMOes
        public ActionResult Index()
        {
            var pRESTAMO = db.PRESTAMO.Include(p => p.ENTIDAD_FINANCIERA).Include(p => p.MONEDA).Include(p => p.USUARIO);
            return View(pRESTAMO.ToList());
        }

        // GET: PRESTAMOes/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMO pRESTAMO = db.PRESTAMO.Find(id);
            if (pRESTAMO == null)
            {
                return HttpNotFound();
            }
            return View(pRESTAMO);
        }

        // GET: PRESTAMOes/Create
        public ActionResult Create()
        {
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDAD_FINANCIERA, "ID_ENTIDAD", "NOMBRE");
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO");
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1");
            return View();
        }

        // POST: PRESTAMOes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_PRESTAMO,ID_ENTIDAD,ID_MONEDA,MONTO_PRESTAMO,PLAZO_DIAS,TASA_INTERES,MODALIDAD_PAGO,FECHA_PRESTAMO,FECHA_VENCIMIENTO,MONTO_INTERESES,ESTADO,FECHA_CREACION,ID_USUARIO_CREA")] PRESTAMO pRESTAMO)
        {
            if (ModelState.IsValid)
            {
                db.PRESTAMO.Add(pRESTAMO);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDAD_FINANCIERA, "ID_ENTIDAD", "NOMBRE", pRESTAMO.ID_ENTIDAD);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", pRESTAMO.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", pRESTAMO.ID_USUARIO_CREA);
            return View(pRESTAMO);
        }

        // GET: PRESTAMOes/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMO pRESTAMO = db.PRESTAMO.Find(id);
            if (pRESTAMO == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDAD_FINANCIERA, "ID_ENTIDAD", "NOMBRE", pRESTAMO.ID_ENTIDAD);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", pRESTAMO.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", pRESTAMO.ID_USUARIO_CREA);
            return View(pRESTAMO);
        }

        // POST: PRESTAMOes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_PRESTAMO,ID_ENTIDAD,ID_MONEDA,MONTO_PRESTAMO,PLAZO_DIAS,TASA_INTERES,MODALIDAD_PAGO,FECHA_PRESTAMO,FECHA_VENCIMIENTO,MONTO_INTERESES,ESTADO,FECHA_CREACION,ID_USUARIO_CREA")] PRESTAMO pRESTAMO)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pRESTAMO).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_ENTIDAD = new SelectList(db.ENTIDAD_FINANCIERA, "ID_ENTIDAD", "NOMBRE", pRESTAMO.ID_ENTIDAD);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", pRESTAMO.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", pRESTAMO.ID_USUARIO_CREA);
            return View(pRESTAMO);
        }

        // GET: PRESTAMOes/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PRESTAMO pRESTAMO = db.PRESTAMO.Find(id);
            if (pRESTAMO == null)
            {
                return HttpNotFound();
            }
            return View(pRESTAMO);
        }

        // POST: PRESTAMOes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            PRESTAMO pRESTAMO = db.PRESTAMO.Find(id);
            db.PRESTAMO.Remove(pRESTAMO);
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
