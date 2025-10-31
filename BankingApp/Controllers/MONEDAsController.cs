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
    public class MONEDAsController : Controller
    {
        private Entities db = new Entities();

        // GET: MONEDAs
        public ActionResult Index()
        {
            return View(db.MONEDA.ToList());
        }

        // GET: MONEDAs/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDA.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // GET: MONEDAs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MONEDAs/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_MONEDA,CODIGO,NOMBRE,SIMBOLO,ESTADO,FECHA_CREACION")] MONEDA mONEDA)
        {
            if (ModelState.IsValid)
            {
                db.MONEDA.Add(mONEDA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mONEDA);
        }

        // GET: MONEDAs/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDA.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // POST: MONEDAs/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_MONEDA,CODIGO,NOMBRE,SIMBOLO,ESTADO,FECHA_CREACION")] MONEDA mONEDA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mONEDA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mONEDA);
        }

        // GET: MONEDAs/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MONEDA mONEDA = db.MONEDA.Find(id);
            if (mONEDA == null)
            {
                return HttpNotFound();
            }
            return View(mONEDA);
        }

        // POST: MONEDAs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            MONEDA mONEDA = db.MONEDA.Find(id);
            db.MONEDA.Remove(mONEDA);
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
