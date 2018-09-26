using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Web.Mvc;
using System.Web.Services.Description;
using Capstone.Models;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Stripe;

namespace Capstone.Controllers
{
    public class MyPickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyPickups
        public ActionResult Index()
        {
            var myPickups = db.MyPickups.Include(m => m.Dog);
            return View(myPickups.ToList());
        }

        // GET: MyPickups/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPickups myPickups = db.MyPickups.Find(id);
            if (myPickups == null)
            {
                return HttpNotFound();
            }
            return View(myPickups);
        }

        // GET: MyPickups/Create
        public ActionResult Create()
        {
            ViewBag.DogId = new SelectList(db.Dogs, "Id", "Name");
            return View();
        }

        // POST: MyPickups/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DogId,DateTime,Frequency")] MyPickups myPickups)
        {
            if (ModelState.IsValid)
            {
                db.MyPickups.Add(myPickups);
                db.SaveChanges();
                return RedirectToAction("Charge");
            }

            ViewBag.DogId = new SelectList(db.Dogs, "Id", "Name", myPickups.DogId);
            return View(myPickups);
        }

        // GET: MyPickups/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPickups myPickups = db.MyPickups.Find(id);
            if (myPickups == null)
            {
                return HttpNotFound();
            }
            ViewBag.DogId = new SelectList(db.Dogs, "Id", "Name", myPickups.DogId);
            return View(myPickups);
        }

        // POST: MyPickups/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DogId,DateTime,Frequency")] MyPickups myPickups)
        {
            if (ModelState.IsValid)
            {
                db.Entry(myPickups).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DogId = new SelectList(db.Dogs, "Id", "Name", myPickups.DogId);
            return View(myPickups);
        }

        // GET: MyPickups/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MyPickups myPickups = db.MyPickups.Find(id);
            if (myPickups == null)
            {
                return HttpNotFound();
            }
            return View(myPickups);
        }

        // POST: MyPickups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            MyPickups myPickups = db.MyPickups.Find(id);
            db.MyPickups.Remove(myPickups);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Charge()
        {
            return View();
        }

        [HttpPost]
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
            chargeService.ApiKey = "sk_test_ptTQd56xfvqKYtyYtux02X4j";

            StripeCharge stripeCharge = chargeService.Create(myCharge);

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
