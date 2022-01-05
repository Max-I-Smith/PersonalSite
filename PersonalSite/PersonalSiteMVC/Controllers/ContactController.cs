using PersonalSiteMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace PersonalSiteMVC.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (!ModelState.IsValid)
            {
                return View(cvm);
            }
            string message = $"You have received an email from {cvm.Name} with a subject of {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br/>{cvm.Message}";
           

            MailMessage mm = new MailMessage(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),

                ConfigurationManager.AppSettings["EmailTo"].ToString(),

                cvm.Subject,

                message
                );


            mm.IsBodyHtml = true;
            mm.Priority = MailPriority.High;


            mm.ReplyToList.Add(cvm.Email);


            SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());


            client.Credentials = new NetworkCredential(
                ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString());
            client.Port = 8889;//Neccessary for some firewalls
            try
            {
                client.Send(mm);
            }
            catch (Exception ex)
            {
                ViewBag.CustomerMessage = $"We are sorry the request could not be sent at this time. Please try again later. <br/> Error Message: <br/> {ex.StackTrace}";
                return View(cvm);
            }


            return View("EmailConfirmation", cvm);
        }
    }
}