using Adopcion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Adopcion.Controllers
{
    public class QuestionController : Controller
    {
        //
        // GET: /Question/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQuestion()
        {

            return View();
        }

        [HttpPost]
        public ActionResult AddQuestion( Question question)
        {
            var pet = new Pet();
            pet.Details(question.pet_ID);
           


            question.AddQuestion();
            string message = question.Interested_Name + " le ha enviado la siguiente consulta: <br />" + question.Interested_Question + " \n Para contactarlo utilice la siguiente informacion: <br /> Email " + question.Interested_Email + "  <br /> Teléfono " + question.Interested_Phone;
            this.SendEmail(pet.Email,"Ha recibido una nueva consulta", message);
            return View("_QuestionSuccess");

        }


        MailMessage mail = new MailMessage();
        private void SendEmail(string EmailAddress, string txtSubject, string message)
        {


            mail.To.Add(EmailAddress);
            mail.From = new MailAddress("adopta.electnet@gmail.com");
            mail.Subject = txtSubject;
            mail.Body = message;


            mail.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("adopta.electnet@gmail.com", "electivo.net");
            smtp.EnableSsl = true;
            smtp.Send(mail);

        }


        public ActionResult DisplayQuestions(long id)
        {

            var question = new Question();
            var questions = question.GetQuestions(id);
            
            return View(questions);
        }


    }
}
