using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ElToko.Models;

namespace ElToko.Controllers
{
    public class TransaksiNasabahsController : Controller
    {
        private Model1 db = new Model1();
      
        // GET: TransaksiNasabahs
        public ActionResult Index()
        {
            var transaksiNasabahs = db.TransaksiNasabahs.Include(t => t.Nasabah);
           
            return View(transaksiNasabahs.ToList());
        }
        public ActionResult Report()
        {
            FormCollection param = new FormCollection();
            return Report(param);
        }
        [HttpPost]
        public ActionResult Report(FormCollection param)
        {
            var transaksiNasabahs = db.TransaksiNasabahs.Include(t => t.Nasabah);
            if (param != null)
            {
                var AccountId = param["AccountId"];
                if (!string.IsNullOrEmpty(AccountId))
                {
                    var idaccount = int.Parse(AccountId);
                    transaksiNasabahs = transaksiNasabahs.Where(p => p.AccountId >= idaccount);

                }
                var StartDate = param["StartDate"];
                if (!String.IsNullOrEmpty(StartDate))
                {
                    var datestart = DateTime.Parse(StartDate);
                    transaksiNasabahs = transaksiNasabahs.Where(p => p.TransactionDate >= datestart);
                }
              
                var EndDate = param["EndDate"];
                if (!string.IsNullOrEmpty(EndDate))
                {
                    var dateend = DateTime.Parse(EndDate);
                    transaksiNasabahs = transaksiNasabahs.Where(p => p.TransactionDate <= dateend);
                }
               


            }
          

            return View(transaksiNasabahs.ToList());
           

           
        }





        public ActionResult ShowPoint()
        {
            DataTable dt = new DataTable();
            var conn = db.Database.Connection;
            conn.Open();

            string sql = @"select temp.AccountId,  sum(temp.point) as point from (

                        select AccountId , 
                        Case 
                        when  
                        (Description = 'Beli Pulsa'  and Amount <= 10000) 
                        then 0 
                        when  
                        (Description = 'Beli Pulsa' 

                        and (Amount > 10000 and Amount <= 30000)) 
                        then Amount/1000
                        when  
                        (Description = 'Beli Pulsa' 

                        and (Amount > 30000 )) 
                        then Amount/1000 *2

                        when  
                        (Description = 'Bayar Listrik' 

                        and (Amount <= 50000 )) 
                        then 0

                        when  
                        (Description = 'Bayar Listrik' 

                        and (Amount > 50000 and Amount <= 100000 )) 
                        then (Amount/2000)
                        when  
                        (Description = 'Bayar Listrik' 

                        and (Amount > 100000 )) 
                        then (Amount/2000 * 2)
                        else 0
                        END as point
 

                         from TransaksiNasabah
                         ) as temp group by temp.AccountId
                         ";

            using (var cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.Text;
               
                var reader =  cmd.ExecuteReader();
                dt.Load(reader);
                reader.Dispose();
            }
             conn.Close();
            return View(dt);
            //return dt;
        }


       
        // GET: TransaksiNasabahs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransaksiNasabah transaksiNasabah = db.TransaksiNasabahs.Find(id);
            if (transaksiNasabah == null)
            {
                return HttpNotFound();
            }
            return View(transaksiNasabah);
        }

        // GET: TransaksiNasabahs/Create
        public ActionResult Create()
        {
            ViewBag.AccountId = new SelectList(db.Nasabahs, "AccountId", "Name");


            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="kredit", Value = "C" },
                new SelectListItem{ Text="debit", Value = "D" },
        
            };

            ViewBag.DebitCreditStatus = list;
            TransaksiNasabah transaksiNasabah = new TransaksiNasabah();
            return View(transaksiNasabah);
        }

        // POST: TransaksiNasabahs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AccountId,TransactionDate,Description,DebitCreditStatus,Amount")] TransaksiNasabah transaksiNasabah)
        {
            if (ModelState.IsValid)
            {
                db.TransaksiNasabahs.Add(transaksiNasabah);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AccountId = new SelectList(db.Nasabahs, "AccountId", "Name", transaksiNasabah.AccountId);
            return View(transaksiNasabah);
        }

        // GET: TransaksiNasabahs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransaksiNasabah transaksiNasabah = db.TransaksiNasabahs.Find(id);
            if (transaksiNasabah == null)
            {
                return HttpNotFound();
            }
            ViewBag.AccountId = new SelectList(db.Nasabahs, "AccountId", "Name", transaksiNasabah.AccountId);
            var list = new List<SelectListItem>
            {
                new SelectListItem{ Text="kredit", Value = "C" },
                new SelectListItem{ Text="debit", Value = "D" },

            };
            ViewBag.DebitCreditStatus = list;
            return View(transaksiNasabah);
        }

        // POST: TransaksiNasabahs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AccountId,TransactionDate,Description,DebitCreditStatus,Amount")] TransaksiNasabah transaksiNasabah)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaksiNasabah).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AccountId = new SelectList(db.Nasabahs, "AccountId", "Name", transaksiNasabah.AccountId);
            return View(transaksiNasabah);
        }

        // GET: TransaksiNasabahs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransaksiNasabah transaksiNasabah = db.TransaksiNasabahs.Find(id);
            if (transaksiNasabah == null)
            {
                return HttpNotFound();
            }
            return View(transaksiNasabah);
        }

        // POST: TransaksiNasabahs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransaksiNasabah transaksiNasabah = db.TransaksiNasabahs.Find(id);
            db.TransaksiNasabahs.Remove(transaksiNasabah);
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
