using System;
using System.Net.Mail;
using System.Net;

namespace fa22LBT.Utilities
{
    public static class EmailMessaging
    {
        public static void SendEmail(String toEmailAddress, String emailSubject, String emailBody)
        {
          
            //This is the address that will be SENDING the emails (the FROM address)
            String strFromEmailAddress = "mis333k.gssj.proj@gmail.com";

            //This is the password for our Gmail account
            String strPassword = "xrvantyvtpmdbbwq";

            //This is the name of the business from which you are sending
            String strCompanyName = "Longhorn Bank";

            //KeyValue senderEmailPassword = _context.KeyValues.FirstOrDefault(i => i.Code == "SenderEmailPassword");

            //if (senderEmailPassword != null)
            //{
            //    strFromEmailAddress = senderEmailPassword.Value;
            //}
            
            //Create an email client to send the emails
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                UseDefaultCredentials = false,
                //This is the SENDING email address and password
                Credentials = new NetworkCredential(strFromEmailAddress, strPassword),
                EnableSsl = true
            };

            //emailBody is passed into the method as a parameter
            String finalMessage = emailBody + "\n\nThanks, \nYour Team 18 Longhorn Bank";

            //Create an email address object for the sender address
            MailAddress senderEmail = new MailAddress(strFromEmailAddress, strCompanyName);

            //Create a new mail message
            MailMessage mm = new MailMessage();

            //Set the subject line of the message
            mm.Subject = "Team 18 - " + emailSubject;

            //Set the sender address
            mm.Sender = senderEmail;

            //Set the from address
            mm.From = senderEmail;

            //Add the recipient (passed in as a parameter) to the list of people receiving the email
            mm.To.Add(new MailAddress(toEmailAddress));

            //Add the message (passed)
            mm.Body = finalMessage;

            //send the message!
            client.Send(mm);
        }
    }
}