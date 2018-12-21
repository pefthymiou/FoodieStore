using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using FoodieStore.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FoodieStore.Tools
{
    public class EmailHandler
    {
        public async Task RegistrationEmail(string receiverMail, string receiverFirstName, string receiverUname, string receiverPass)
        {
            var smtp = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@foodie.com", "Foodie Team"),
                Subject = "Welcome to Foodie!",
                HtmlContent = $"<p><strong>Καλώς ήρθες στο Foodie!</strong></p> Γεια σου <b>{ receiverFirstName }</b>, ευχαριστούμε για την εγγραφή σου στο Foodie, το μοναδικό site όπου μπορείς να βρεις αληθινό σπιτικό φαγητό και να το έχεις στο χώρο σου σε λίγα λεπτά.<br>Τέρμα το μαγείρεμα, τέρμα το junk food ώρα για foodie!<hr>Tα στοιχεία της εγγραφής σου είναι <br><b>Username: </b><i>{ receiverUname}</i><br><b>Password: </b><i>{ receiverPass}</i> <br> <div align=\"center\"><p><b>The Foodie Team</b></p></div>",
            };
            msg.AddTo(new EmailAddress($"{receiverMail}", $"{receiverFirstName}"));
            var respone = await smtp.SendEmailAsync(msg);
        }


        public async Task OrderEmail(Order receivedOrder)
        {
            var smtp = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@foodie.com", "Foodie Team"),
                Subject = $"Foodie - Παραγγελία {receivedOrder.OrderId}",
                HtmlContent = $"<p><strong>Παραγγελία {receivedOrder.OrderId}!</strong><br></p> Γεια σου <b>{ receivedOrder.FirstName }</b> <br> Σε ευχαριστούμε που επέλεξες το Foodie! H παραγγελία σου ετοιμάζεται και σε μερικά λεπτά θα έχεις το email για την ώρα παραλαβής!<br>Μέχρι τότε keep fooding!<hr><div align=\"center\"><p><b>The Foodie Team</b></p></div>",
            };
            msg.AddTo(new EmailAddress($"{receivedOrder.Email}", $"{receivedOrder.FirstName}"));
            var respone = await smtp.SendEmailAsync(msg);
        }

        public async Task CompletedEmail(Order completededOrder)
        {
            var smtp = new SendGridClient("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");
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("info@foodie.com", "Foodie Team"),
                Subject = $"Foodie - H παραγγελία {completededOrder.OrderId}",
                HtmlContent = $"<p><strong>Παραγγελία {completededOrder.OrderId}!</strong><br></p> <b>{ completededOrder.FirstName }</b> H παραγγελία σου μόλις έφυγε και θα είναι στην πόρτα σου σε περίπου 30 λεπτά! Kαλή όρεξη!<br><hr><div align=\"center\"><p><b>The Foodie Team</b></p></div>",
            };
            msg.AddTo(new EmailAddress($"{completededOrder.Email}", $"{completededOrder.FirstName}"));
            var respone = await smtp.SendEmailAsync(msg);
        }
    }
}