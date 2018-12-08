using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.ServiceModel.Channels;
using System.Web.Mvc;
using System.Web.Services.Description;
using Capstone.Models;
using Google.Apis.Gmail.v1;
using Google.Apis.Gmail.v1.Data;
using Microsoft.AspNet.Identity;
using Stripe;

namespace Capstone.Controllers
{
    public class MyPickupsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MyPickups
        public ActionResult Index()
        {

            List<MyPickups> pickups = new List<MyPickups>();
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var currentCustomer = db.Customers.Where(c => c.ApplicationUserId == currentUser).FirstOrDefault();
            var myDogs = db.Dogs.Where(d => d.CustomerId == currentCustomer.Id).Select(d => d.Id).ToList();

            foreach (int dog in myDogs)
            {
                var currentDog = db.Dogs.Where(d => d.Id == dog).Select(d => d.Id).FirstOrDefault();
                var dogPickup = db.MyPickups.Where(m => m.DogId == currentDog).Include(m => m.Dog).Include(m => m.Employee).FirstOrDefault();
                if (dogPickup != null)
                {
                    pickups.Add(dogPickup);
                }


            }

            return View(pickups);



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
        public ActionResult Create([Bind(Include = "Id,DogId,DayOfTheWeek,Frequency,Time,EmployeeId")] MyPickups myPickups)
        {
            

            if (myPickups.DayOfTheWeek == "Monday")
            {
                var employeeAvailabilityWeekdays = db.Employees.Where(e => e.Availability == "Weekdays").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekdays.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekdays[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Tuesday")
            {
                var employeeAvailabilityWeekdays = db.Employees.Where(e => e.Availability == "Weekdays").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekdays.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekdays[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Wednesday")
            {
                var employeeAvailabilityWeekdays = db.Employees.Where(e => e.Availability == "Weekdays").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekdays.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekdays[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Thursday")
            {
                var employeeAvailabilityWeekdays = db.Employees.Where(e => e.Availability == "Weekdays").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekdays.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekdays[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Friday")
            {
                var employeeAvailabilityWeekdays = db.Employees.Where(e => e.Availability == "Weekdays").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekdays.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekdays[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Saturday")
            {
                var employeeAvailabilityWeekends = db.Employees.Where(e => e.Availability == "Weekends").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekends.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekends[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            else if (myPickups.DayOfTheWeek == "Sunday")
            {
                var employeeAvailabilityWeekends = db.Employees.Where(e => e.Availability == "Weekends").Select(e => e).ToList();
                int employeeCount = employeeAvailabilityWeekends.Count;
                Random randomEmployee = new Random();
                int employeeAtIndex = randomEmployee.Next(1, employeeCount);
                var selectedEmployee = employeeAvailabilityWeekends[employeeAtIndex];
                myPickups.EmployeeId = selectedEmployee.Id;
            }
            if (!ModelState.IsValid)
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var pickupToUpdate = db.Customers.Find(id);
            if (TryUpdateModel(pickupToUpdate, "",
                new string[] { "DayOfTheWeek", "Frequency" }))
            {
                try
                {
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (DataException)
                {
                    ModelState.AddModelError("", "Unable to save changes. Please try again.");
                }
            }
            return View(pickupToUpdate);
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



            return RedirectToAction("SendEmailToEmployee", "Email");
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
