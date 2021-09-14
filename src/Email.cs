using System.Threading.Tasks;
using FluentEmail.Smtp; // nuget dependency
using FluentEmail.Core;// nuget dependency
using System.Net.Mail;
using System.Net;
using SendGrid;
using System;
using SendGrid.Helpers.Mail;
//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 api key- sendgrid
namespace Inoa
{
     public class EmailSender
    {

        public EmailSender(){

        }
        public async Task SendEmail(string stock, string action){

            string msg = "";
            if(action=="buy"){
                msg=$"You should buy stocks of {stock}";

            }else{
                msg=$"You should sell stocks of {stock}";
            }
            
            
            try{ //Working with delay
                 var sendGridClient = new SendGridClient("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");
            var from = new EmailAddress("jteste899@outlook.com", "Jean");
            var subject = "Stock-alert";
            var to = new EmailAddress("jeancarlodev@gmail.com", "Jean");
            var plainContent = "Changes in Stock";
            var htmlContent = $"<h1>Hello, {msg}</h1>";
            var mailMessage = MailHelper.CreateSingleEmail(from, to, subject, plainContent, htmlContent);
            await sendGridClient.SendEmailAsync(mailMessage);
            Console.WriteLine("Email sent");
            }catch(Exception e){
                Console.WriteLine(e.Message);
            }
        
         
             
        }
    }
}