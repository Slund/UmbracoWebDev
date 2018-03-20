using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Umbraco.Core.Models;
using Umbraco.Web.Mvc;
using AarhusWebDevCoop.ViewModels;
using System.Net.Mail;

namespace AarhusWebDevCoop.Controllers
{
    public class ContactFormSurfaceController : SurfaceController
    {
        // GET: ContactFormSurface
        public ActionResult Index()
        {
            ContactForm model = new ContactForm();

            return PartialView("ContactForm", model);
        }
        // HELLO2

        [HttpPost]
        public ActionResult HandleFormSubmit(ContactForm model)
        {
            if (!ModelState.IsValid)
                return CurrentUmbracoPage();

            MailMessage message = new MailMessage();
            message.To.Add("slundmain@gmail.com");
            message.Subject = model.Subject;
            message.From = new MailAddress(model.Email, model.Name);
            message.Body = model.Message;

            using (SmtpClient smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("supernemathuske@gmail.com", "Hejumbraco123");
                smtp.EnableSsl = true;
                // send mail
                smtp.Send(message);
            }

            IContent comment = Services.ContentService.CreateContent(model.Subject, CurrentPage.Id, "message");

            comment.SetValue("messageName", model.Name);
            comment.SetValue("email", model.Email);
            comment.SetValue("subject", model.Subject);
            comment.SetValue("messageContent", model.Message);

            // Save
            Services.ContentService.Save(comment);


            TempData["success"] = true;
            return RedirectToCurrentUmbracoPage();
        }
    }
}