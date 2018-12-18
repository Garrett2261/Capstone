using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Properties;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using Twilio.TwiML;
using Twilio.AspNet.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class NotificationController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Notification
        public ActionResult PickupNotification(TextInfo textMessage)
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var employeeIds = db.Employees.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var pickupDog = db.MyPickups.Where(m => m.EmployeeId == employeeIds).OrderByDescending(m => m.Id).FirstOrDefault();
            var dogOwner = db.Dogs.Where(d => d.Id == pickupDog.DogId).Select(d => d.CustomerId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == dogOwner).FirstOrDefault();
            var customerphone = customer.PhoneNumber.ToString();
            var text = textMessage.Message;
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(customerphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "The employee has picked up your dog.");



            return RedirectToAction("Index", "Employees");
        }

        public ActionResult VetNotification(TextInfo textMessage)
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var employeeIds = db.Employees.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var pickupDog = db.MyPickups.Where(m => m.EmployeeId == employeeIds).OrderByDescending(m => m.Id).FirstOrDefault();
            var dogOwner = db.Dogs.Where(d => d.Id == pickupDog.DogId).Select(d => d.CustomerId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == dogOwner).FirstOrDefault();
            var customerphone = customer.PhoneNumber.ToString();
            var text = textMessage.Message;
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(customerphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Your dog is now at the Vet.");

            return RedirectToAction("Index", "Employees");
        }

        public ActionResult ReturnNotification(TextInfo textMessage)
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var employeeIds = db.Employees.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var pickupDog = db.MyPickups.Where(m => m.EmployeeId == employeeIds).OrderByDescending(m => m.Id).FirstOrDefault();
            var dogOwner = db.Dogs.Where(d => d.Id == pickupDog.DogId).Select(d => d.CustomerId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == dogOwner).FirstOrDefault();
            var customerphone = customer.PhoneNumber.ToString();
            var text = textMessage.Message;
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(customerphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Heading back with your dog now");

            return RedirectToAction("Index", "Employees");
        }

        public ActionResult DroppedOffNotification(TextInfo textMessage)
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var employeeIds = db.Employees.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var pickupDog = db.MyPickups.Where(m => m.EmployeeId == employeeIds).OrderByDescending(m => m.Id).FirstOrDefault();
            var dogOwner = db.Dogs.Where(d => d.Id == pickupDog.DogId).Select(d => d.CustomerId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == dogOwner).FirstOrDefault();
            var customerphone = customer.PhoneNumber.ToString();
            var text = textMessage.Message;
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(customerphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Just dropped off you dog. Thanks for choosing Picker Pupper.");

            return RedirectToAction("Index", "Employees");
        }
    }
}