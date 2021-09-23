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
        private string msg{
            get;
            set;
        }
        public Email(){

        }
        public async Task Send(string stock, string action){

            if(action=="buy"){
                this.msg=$"You should buy stocks of {stock}";

            }else{
               this.msg=$"You should sell stocks of {stock}";
            }

       

            try{
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

                 await smtpClient.SendMailAsync(mailMessage);

                Console.WriteLine("Seu email foi enviado com sucesso! :)");
                
              }  catch (Exception e)
            {   
                Console.WriteLine("Houve um erro no envio do email :(");
                Console.WriteLine(e.Message);
                 Console.WriteLine(e);
                Console.ReadLine();
            }
             
        }
    }
}