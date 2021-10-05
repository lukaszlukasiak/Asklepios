﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Asklepios.Web.Areas.HomeArea.Models;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;


namespace Asklepios.Web.ServiceClasses
{
    public static class MailServices
    {
        public static bool CreateAndSendMail(ContactMessageViewModel model)
        {
            try
            {
                MimeMessage mimeMessage= CreateMail(model);
                SendEMail(mimeMessage);
                return true;
            }
            catch (Exception e)
            {
                
                return false;
            }
        }
        public static MimeMessage CreateMail(ContactMessageViewModel model)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(model.ContactEMailAddress));
            email.To.Add(MailboxAddress.Parse("grupa.asklepios@gmail.com"));
            email.Subject = model.Subject;
            string from = "<h2>Sender name: " + model.ContactName + "</h2>";
            string eAddress= "<h2> Sender e-address: " + model.ContactEMailAddress;
            string phone= "<h2> Sender phone number: " + model.PhoneNumber;
            string subject= "<h2> Subject: " + model.Subject;
            string mess = "<h1>Message from client</h1>"+from + eAddress + phone + subject +"<p>" + model.Message + "</p>";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = mess };

            // send email
            return email;
        }

        public static void SendEMail(MimeMessage message)
        {
            using var smtp = new SmtpClient();
            //name Dell Price
            smtp.CheckCertificateRevocation = false;
          //  smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
           // smtp.Authenticate("dell.price11@ethereal.email", "YsuyBaAUduRuxr9YVh");


            smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("grupa.asklepios@gmail.com", "147852qW");

            smtp.Send(message);
            smtp.Disconnect(true);

        }
    }
}
