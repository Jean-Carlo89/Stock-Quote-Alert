using System.Threading.Tasks;
using FluentEmail.Smtp; // nuget dependency
using FluentEmail.Core;// nuget dependency
using System.Net.Mail;
using System.Net;
using SendGrid;
using System;
using SendGrid.Helpers.Mail;
using System.Text;

namespace StockQuoteAlert
{
     public class Email
    {
        static string msg{
            get;
            set;
        }
        public Email(){

        }


        public class EmailBody{
           public MailMessage message {get;set;}
           public SmtpClient client {get;set;}
        }

        public EmailBody CreateMailBody(string stock, string action){
            if(action=="buy"){
                msg=$"You should buy stocks of {stock}";

            }else{
                msg=$"You should sell stocks of {stock}";
            }

             MailMessage mailMessage = new MailMessage($"{Environment.GetEnvironmentVariable("SENDER_EMAIL")}", $"{Environment.GetEnvironmentVariable("RECEIVER_EMAIL")}");
           
                mailMessage.Subject = "Stock-alert";
                mailMessage.IsBodyHtml = true;
                mailMessage.Body = $"<p> {msg} </p>";
                mailMessage.SubjectEncoding = Encoding.GetEncoding("UTF-8");
                mailMessage.BodyEncoding = Encoding.GetEncoding("UTF-8");

                SmtpClient smtpClient = new SmtpClient($"{Environment.GetEnvironmentVariable("SMTP_SERVER")}",int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT")));
    
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential($"{Environment.GetEnvironmentVariable("SENDER_EMAIL")}",$"{Environment.GetEnvironmentVariable("SENDER_PASSWORD_CREDENTIAL")}");

                smtpClient.EnableSsl =Convert.ToBoolean(Environment.GetEnvironmentVariable("SMTP_SSL"));

            EmailBody body = new EmailBody();
            body.message = mailMessage;
            body.client = smtpClient;

            return body;

        }


        public async Task Send(EmailBody mail){

        var smtpClient = mail.client;
        var mailMessage = mail.message;

            try{
                 await smtpClient.SendMailAsync(mailMessage);

                Console.WriteLine("Seu email foi enviado com sucesso! :)");
                
              }  catch (Exception e)
            {   
                Console.WriteLine("Houve um erro no envio do email :(");
                Console.WriteLine(e.Message);
            }
             
        }
    }
}