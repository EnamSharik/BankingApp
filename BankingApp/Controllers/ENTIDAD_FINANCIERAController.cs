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
    public class ENTIDAD_FINANCIERAController : Controller
    {
        private Entities db = new Entities();

        // GET: ENTIDAD_FINANCIERA
        public ActionResult Index()
        {
            return View(db.ENTIDAD_FINANCIERA.ToList());
        }

        // GET: ENTIDAD_FINANCIERA/Details/5
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA = db.ENTIDAD_FINANCIERA.Find(id);
            if (eNTIDAD_FINANCIERA == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDAD_FINANCIERA);
        }

        // GET: ENTIDAD_FINANCIERA/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ENTIDAD_FINANCIERA/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NOMBRE,CODIGO,DIRECCION,TELEFONO,EMAIL,FECHA_REGISTRO,ESTADO")] ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA)
        {
            if (ModelState.IsValid)
            {
                var maxId = db.ENTIDAD_FINANCIERA.Any() ? db.ENTIDAD_FINANCIERA.Max(c => c.ID_ENTIDAD) : 0;
                eNTIDAD_FINANCIERA.ID_ENTIDAD = maxId + 1;

                db.ENTIDAD_FINANCIERA.Add(eNTIDAD_FINANCIERA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eNTIDAD_FINANCIERA);
        }

        // GET: ENTIDAD_FINANCIERA/Edit/5
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA = db.ENTIDAD_FINANCIERA.Find(id);
            if (eNTIDAD_FINANCIERA == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDAD_FINANCIERA);
        }

        // POST: ENTIDAD_FINANCIERA/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_ENTIDAD,NOMBRE,CODIGO,DIRECCION,TELEFONO,EMAIL,FECHA_REGISTRO,ESTADO")] ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(eNTIDAD_FINANCIERA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eNTIDAD_FINANCIERA);
        }

        // GET: ENTIDAD_FINANCIERA/Delete/5
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA = db.ENTIDAD_FINANCIERA.Find(id);
            if (eNTIDAD_FINANCIERA == null)
            {
                return HttpNotFound();
            }
            return View(eNTIDAD_FINANCIERA);
        }

        // POST: ENTIDAD_FINANCIERA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            ENTIDAD_FINANCIERA eNTIDAD_FINANCIERA = db.ENTIDAD_FINANCIERA.Find(id);
            db.ENTIDAD_FINANCIERA.Remove(eNTIDAD_FINANCIERA);
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
