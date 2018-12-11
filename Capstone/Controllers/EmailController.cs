using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;
using Capstone;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;
using RestSharp;
using System.Web.UI.WebControls;
using System.Runtime.Serialization;

namespace Capstone.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> SendEmailToEmployee(EmailInformation emailInformation)
        {
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var currentCustomer = db.Customers.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var dogPickup = db.MyPickups.Where(m => m.Dog.CustomerId == currentCustomer).Where(m => m.DogId == m.Dog.Id).OrderByDescending(m => m.Id).First();
            var employeeEmail = db.Employees.Where(e => e.Id == dogPickup.EmployeeId).Select(e => e.Email).FirstOrDefault();
            var employeeFirstName = db.Employees.Where(e => e.Id == dogPickup.EmployeeId).Select(e => e.FirstName).FirstOrDefault();
            var employeeLastName = db.Employees.Where(e => e.Id == dogPickup.EmployeeId).Select(e => e.LastName).FirstOrDefault();
            var currentCustomerLoggedIn = db.Customers.Where(m => m.ApplicationUserId == currentUser).FirstOrDefault();
            var currentCustomerEmail = currentCustomerLoggedIn.Email;
            var currentCustomerFirstName = currentCustomerLoggedIn.FirstName;
            var currentCustomerLastName = currentCustomerLoggedIn.LastName;
            var subject = emailInformation.Subject;
            var message = emailInformation.Message;
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(currentCustomerEmail, (currentCustomerFirstName + currentCustomerLastName)),
                Subject = subject,
                PlainTextContent = "Elmwood is the best place in the world to me",
                HtmlContent = message
            };
            msg.AddTo(new EmailAddress(employeeEmail, (employeeFirstName + employeeLastName)));
            var response = await client.SendEmailAsync(msg);
            return View();
        }

        public async Task<ActionResult> SendEmailToCustomer(EmailInformation emailInformation)
        {
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var employee = db.Employees.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var dogPickup = db.MyPickups.Where(m => m.EmployeeId == employee).Select(m => m.DogId).FirstOrDefault();
            var dogOwner = db.Dogs.Where(d => d.Id == dogPickup).Select(d => d.CustomerId).FirstOrDefault();
            var customer = db.Customers.Where(c => c.Id == dogOwner).FirstOrDefault();
            var customerEmail = customer.Email;
            var customerFirstName = customer.FirstName;
            var customerLastName = customer.LastName;

            var currentEmployee = db.Employees.Where(m => m.ApplicationUserId == currentUser).FirstOrDefault();
            var currentEmployeeEmail = currentEmployee.Email;
            var currentEmployeeFirstName = currentEmployee.FirstName;
            var currentEmployeeLastName = currentEmployee.LastName;
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var subject = emailInformation.Subject;
            var message = emailInformation.Message;
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(currentEmployeeEmail, (currentEmployeeFirstName + currentEmployeeLastName)),
                Subject = subject,
                PlainTextContent = "Elmwood is the best place in the world to me",
                HtmlContent = message

            };
            msg.AddTo(new EmailAddress(customerEmail, (customerFirstName + customerLastName)));
            var response = await client.SendEmailAsync(msg);
            return View();
        }

        public async Task<ActionResult> ConfirmationEmail()
        {
            var loggedInCurrentUsername = User.Identity.Name;
            var loggedInCurrentUser = db.Users.Where(m => m.UserName == loggedInCurrentUsername).Select(m => m.Id).FirstOrDefault();
            var loggedInCurrentCustomer = db.Customers.Where(m => m.ApplicationUserId == loggedInCurrentUser).Select(m => m.Id).FirstOrDefault();
            var dogPickup = db.MyPickups.Where(m => m.Dog.CustomerId == loggedInCurrentCustomer).Where(m => m.DogId == m.Dog.Id).OrderByDescending(m => m.Id).First();
            var dogName = db.Dogs.Where(d => d.Id == dogPickup.DogId).Select(d => d.Name).First();
            var pickupDayOfTheWeek = db.MyPickups.Where(m => m.Id == dogPickup.Id).Select(m => m.Time).First();
            var pickupTime = pickupDayOfTheWeek.ToLongDateString();
            var pickupEmployeeFirstName = db.Employees.Where(e => e.Id == dogPickup.EmployeeId).Select(e => e.FirstName).First();
            var pickupEmployeeLastName = db.Employees.Where(e => e.Id == dogPickup.EmployeeId).Select(e => e.LastName).First();


            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var currentCustomer = db.Customers.Where(m => m.ApplicationUserId == currentUser).FirstOrDefault();
            var currentCustomerEmail = currentCustomer.Email.ToString();
            var currentCustomerFirstName = currentCustomer.FirstName.ToString();
            var currentCustomerLastName = currentCustomer.LastName.ToString();
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var subject = "Your Pickup";
            var message = "Your Pickup has been added. Your dog, " + dogName + ", will be picked up on" + pickupTime + "by" + pickupEmployeeFirstName + pickupEmployeeLastName + ". Thank you for choosing Picker Pupper and we hope you choose us again!";
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("PickerPupper@dogs.com"),
                Subject = subject,
                PlainTextContent = "Elmwood is the best place in the world to me",
                HtmlContent = message

            };
            msg.AddTo(new EmailAddress(currentCustomerEmail, (currentCustomerFirstName + currentCustomerLastName)));
            var response = await client.SendEmailAsync(msg);
            return RedirectToAction("SendEmailToEmployee", "Email");
        }
    }
}