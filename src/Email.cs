using System.Threading.Tasks;
using FluentEmail.Smtp; // nuget dependency
using FluentEmail.Core;// nuget dependency
using System.Net.Mail;
namespace Inoa
{
     public class EmailSender
    {

        public EmailSender(){

        }
        public async Task SendEmail(){
            var sender = new SmtpSender(()=> new SmtpClient(host:"localhost")
            {
                EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory,
                PickupDirectoryLocation=@"S:\Demos"
            });

            Email.DefaultSender = sender; 

            var email = await Email
            .From(emailAddress:"exemplo@exemplo.com")
            .To(emailAddress:"test@test.com", name:"Sue")
            .Subject(subject:"thanks")
            .Body(body:"Thanks for aaa")
            .SendAsync();

             
        }
    }
}