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
    public class INVERSIONsController : Controller
    {
        private Entities db = new Entities();

        // GET: INVERSIONs
        public ActionResult Index()
        {
            var iNVERSION = db.INVERSION.Include(i => i.CLIENTE).Include(i => i.MONEDA).Include(i => i.USUARIO);
            return View(iNVERSION.ToList());
        }

        // GET: INVERSIONs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSION iNVERSION = db.INVERSION.Find(id);
            if (iNVERSION == null)
            {
                return HttpNotFound();
            }
            return View(iNVERSION);
        }

        // GET: INVERSIONs/Create
        public ActionResult Create()
        {
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE");
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO");
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1");
            return View();
        }

        // POST: INVERSIONs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_CLIENTE,ID_MONEDA,MONTO_INVERSION,PLAZO_DIAS,TASA_INTERES,MODALIDAD_PAGO,FECHA_VENCIMIENTO,MONTO_INTERESES,ID_USUARIO_CREA")] INVERSION iNVERSION)
        {
            if (ModelState.IsValid)
            {
                var nextId = db.INVERSION.Any() ? db.INVERSION.Max(n => n.ID_INVERSION + 1) : 0;
                iNVERSION.ID_INVERSION = nextId;
                var today = DateTime.Today;
                iNVERSION.FECHA_CREACION = today;
                iNVERSION.FECHA_INVERSION = today;
                iNVERSION.ESTADO = "A";
                iNVERSION.CLIENTE = db.CLIENTE.Find(iNVERSION.ID_CLIENTE);
                iNVERSION.MONEDA = db.MONEDA.Find(iNVERSION.ID_MONEDA);
                iNVERSION.USUARIO = db.USUARIO.Find(iNVERSION.ID_USUARIO_CREA);
                db.INVERSION.Add(iNVERSION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE", iNVERSION.ID_CLIENTE);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", iNVERSION.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", iNVERSION.ID_USUARIO_CREA);
            return View(iNVERSION);
        }

        // GET: INVERSIONs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSION iNVERSION = db.INVERSION.Find(id);
            if (iNVERSION == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE", iNVERSION.ID_CLIENTE);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", iNVERSION.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", iNVERSION.ID_USUARIO_CREA);
            return View(iNVERSION);
        }

        // POST: INVERSIONs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_INVERSION,ID_CLIENTE,ID_MONEDA,MONTO_INVERSION,PLAZO_DIAS,TASA_INTERES,MODALIDAD_PAGO,FECHA_INVERSION,FECHA_VENCIMIENTO,MONTO_INTERESES,ESTADO,FECHA_CREACION,ID_USUARIO_CREA")] INVERSION iNVERSION)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iNVERSION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_CLIENTE = new SelectList(db.CLIENTE, "ID_CLIENTE", "NOMBRE", iNVERSION.ID_CLIENTE);
            ViewBag.ID_MONEDA = new SelectList(db.MONEDA, "ID_MONEDA", "CODIGO", iNVERSION.ID_MONEDA);
            ViewBag.ID_USUARIO_CREA = new SelectList(db.USUARIO, "ID_USUARIO", "USUARIO1", iNVERSION.ID_USUARIO_CREA);
            return View(iNVERSION);
        }

        // GET: INVERSIONs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            INVERSION iNVERSION = db.INVERSION.Find(id);
            if (iNVERSION == null)
            {
                return HttpNotFound();
            }
            return View(iNVERSION);
        }

        // POST: INVERSIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            INVERSION iNVERSION = db.INVERSION.Find(id);
            db.INVERSION.Remove(iNVERSION);
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
