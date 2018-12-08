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

namespace Capstone.Controllers
{
    public class EmailController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public async Task<ActionResult> SendEmailToEmployee(EmailInformation emailInformation)
        {
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var customer = db.Customers.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var customerDog = db.Dogs.Where(d => d.CustomerId == customer).Select(d => d.Id).FirstOrDefault();
            var dog = db.MyPickups.Where(m => m.DogId == customerDog).Select(m => m.EmployeeId).FirstOrDefault();
            var employee = db.Employees.Where(e => e.Id == dog).FirstOrDefault();
            var employeeEmail = employee.Email.ToString();
            var employeeFirstName = employee.FirstName.ToString();
            var employeeLastName = employee.LastName.ToString();

            var currentCustomer = db.Customers.Where(m => m.ApplicationUserId == currentUser).FirstOrDefault();
            var currentCustomerEmail = currentCustomer.Email;
            var currentCustomerFirstName = currentCustomer.FirstName;
            var currentCustomerLastName = currentCustomer.LastName;
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var subject = emailInformation.Subject;
            var message = emailInformation.Message;
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
            var currentUsername = User.Identity.Name;
            var currentUser = db.Users.Where(m => m.UserName == currentUsername).Select(m => m.Id).FirstOrDefault();
            var customer = db.Customers.Where(m => m.ApplicationUserId == currentUser).Select(m => m.Id).FirstOrDefault();
            var customerDog = db.Dogs.Where(d => d.CustomerId == customer).Select(d => d.Id).FirstOrDefault();
            var dog = db.MyPickups.Where(m => m.DogId == customerDog).Select(m => m.EmployeeId).FirstOrDefault();
            var employee = db.Employees.Where(e => e.Id == dog).FirstOrDefault();
            var employeeEmail = employee.Email.ToString();
            var employeeFirstName = employee.FirstName.ToString();
            var employeeLastName = employee.LastName.ToString();

            var currentCustomer = db.Customers.Where(m => m.ApplicationUserId == currentUser).FirstOrDefault();
            var currentCustomerEmail = currentCustomer.Email;
            var currentCustomerFirstName = currentCustomer.FirstName;
            var currentCustomerLastName = currentCustomer.LastName;
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var subject = "Your Pickup";
            //var message = ("Thank you for choosing Picker Pupper. Your dog will be picked up on" + object from mypickups database that gets the day of the week + "at" + Time from mypickups database record + ". Thank you choosing Picker Pupper. The application where we'll pick up your pupper and send he or she to the vet.");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(currentCustomerEmail, (currentCustomerFirstName + currentCustomerLastName)),
                Subject = subject,
                PlainTextContent = "Elmwood is the best place in the world to me",
                HtmlContent = "message"

            };
            msg.AddTo(new EmailAddress(employeeEmail, (employeeFirstName + employeeLastName)));
            var response = await client.SendEmailAsync(msg);
            return View();
        }
    }
}