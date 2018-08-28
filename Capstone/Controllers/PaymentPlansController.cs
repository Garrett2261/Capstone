using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using System.Threading.Tasks;
using Stripe;


namespace Capstone.Controllers
{
    public class PaymentPlansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: PaymentPlans
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaymentPlans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentPlan paymentPlan = db.PaymentPlan.Find(id);
            if (paymentPlan == null)
            {
                return HttpNotFound();
            }
            return View(paymentPlan);
        }

        public ActionResult Charge()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Charge(string stripeToken, string stripeEmail)
        {
            var myCharge = new StripeChargeCreateOptions();

            myCharge.Amount = 1000;
            myCharge.Currency = "usd";

            myCharge.ReceiptEmail = stripeEmail;
            myCharge.Description = "Test Charge";
            myCharge.SourceTokenOrExistingSourceId = stripeToken;
            myCharge.Capture = true;

            var chargeService = new StripeChargeService();
            StripeCharge stripeCharge = chargeService.Create(myCharge);

            return View();
        }

        

        // GET: PaymentPlans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PaymentPlans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Token,Amount,CardHolderName")] PaymentPlan paymentPlan)
        {
            if (ModelState.IsValid)
            {
                db.PaymentPlan.Add(paymentPlan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(paymentPlan);
        }

        // GET: PaymentPlans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentPlan paymentPlan = db.PaymentPlan.Find(id);
            if (paymentPlan == null)
            {
                return HttpNotFound();
            }
            return View(paymentPlan);
        }

        // POST: PaymentPlans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Token,Amount,CardHolderName")] PaymentPlan paymentPlan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(paymentPlan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(paymentPlan);
        }

        // GET: PaymentPlans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PaymentPlan paymentPlan = db.PaymentPlan.Find(id);
            if (paymentPlan == null)
            {
                return HttpNotFound();
            }
            return View(paymentPlan);
        }

        // POST: PaymentPlans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PaymentPlan paymentPlan = db.PaymentPlan.Find(id);
            db.PaymentPlan.Remove(paymentPlan);
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
