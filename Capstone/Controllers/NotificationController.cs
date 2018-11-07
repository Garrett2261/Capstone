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
        public ActionResult PickupNotification()
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            var dogname = "";
            var dog = db.Dogs.Where(d => d.Name == dogname).SingleOrDefault();
            var custphone = db.Customers.Where(c => c.Id == dog.CustomerId).Single().PhoneNumber.ToString();
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(custphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Your dog has been picked up. I am taking him to the vet now.");

            return Content(message.Sid);
        }

        public ActionResult VetNotification()
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            string dogname = "";
            var dog = db.Dogs.Where(d => d.Name == dogname).SingleOrDefault();
            var custphone = db.Customers.Where(c => c.Id == dog.CustomerId).Single().PhoneNumber.ToString();
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(custphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Just got to the vet with your dog. The vet is now looking at your dog.");

            return Content(message.Sid);
        }

        public ActionResult ReturnNotification()
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            string dogname = "";
            var dog = db.Dogs.Where(d => d.Name == dogname).SingleOrDefault();
            var custphone = db.Customers.Where(c => c.Id == dog.CustomerId).Single().PhoneNumber.ToString();
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(custphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "The visit is over now. Heading back home with your dog.");

            return Content(message.Sid);
        }

        public ActionResult DroppedOffNotification()
        {
            var accountSID = Settings.Default.AccountSID;
            var authToken = Settings.Default.AuthToken;
            string dogname = "";
            var dog = db.Dogs.Where(d => d.Name == dogname).SingleOrDefault();
            var custphone = db.Customers.Where(c => c.Id == dog.CustomerId).Single().PhoneNumber.ToString();
            TwilioClient.Init(accountSID, authToken);

            var to = new PhoneNumber(custphone);
            var from = new PhoneNumber("+14142690794");

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Your dog is now home. I just dropped him off.");

            return Content(message.Sid);
        }
    }
}