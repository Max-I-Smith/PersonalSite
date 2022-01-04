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
        public ActionResult Contact(ContactViewModel cvm)
        {
            if (ModelState.IsValid)
            {
                string body = $"{cvm.Name} has sent you the following message:<br/>" +
                    $"{cvm.Message}<br/> from the following email:<br/>{cvm.Email}";

                MailMessage mm = new MailMessage(
                    ConfigurationManager.AppSettings["EmailUser"].ToString(),

                    ConfigurationManager.AppSettings["EmailTo"].ToString(),
                    "",
                    body);
                mm.IsBodyHtml = true;

                SmtpClient client = new SmtpClient(ConfigurationManager.AppSettings["EmailClient"].ToString());

                client.Credentials = new NetworkCredential(ConfigurationManager.AppSettings["EmailUser"].ToString(),
                ConfigurationManager.AppSettings["EmailPass"].ToString());

                try
                {
                    client.Send(mm);
                }
                catch(Exception ex)
                {
                    ViewBag.CustomMessage =
                        $"I'm sorry your request could not be completed at this time." +
                        $"  Please try again later. Error Message: <br/> {ex.StackTrace}";
                    return View(cvm);
                }
                return View("EmailConfirmation", cvm);
                }
            return View(cvm);
        }
    }
}