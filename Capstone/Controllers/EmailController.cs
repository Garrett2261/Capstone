using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Models;

namespace Capstone.Controllers
{
    public class EmailController : Controller
    {
        private SendGridEmailService _sendGridEmailService;
        public EmailController()
        {
            _sendGridEmailService = new SendGridEmailService();
        }
        // GET: Email
        public ActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Send(EmailContract emailContract)
        {
            try
            {
                var response = _sendGridEmailService.Send(emailContract);
                ViewBag.Success = true;
                return View();
            }
            catch (Exception)
            {
                ViewBag.Success = false;
                return View();
            }
        }
    }
}