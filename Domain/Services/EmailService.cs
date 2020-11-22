using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Application.Dtos;
using Domain.Repositorios;
using Microsoft.Extensions.Options;

namespace Domain.Services
{
    public class EmailService : IEmailService
    {
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public EmailSettings _emailSettings { get; }
        public Task SendConfirmationEmailAsync(string email, string subject, string message,string confirmationLink)
        {
            try
            {
                
                var body = createTemplateConfirmEmail(message,confirmationLink);

                
                Execute(email, subject, body).Wait();
                return Task.FromResult(0);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task Execute(string email, string subject, string message)
        {
            try
            {
                string toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

                MailMessage mail = new MailMessage()
                {
                    From = new MailAddress(_emailSettings.UsernameEmail, "MyConsulta - Email")
                };

               mail.To.Add(new MailAddress(toEmail));
                //mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.High;

                //outras opções
                //mail.Attachments.Add(new Attachment(arquivo));
                //

                using (SmtpClient smtp = new SmtpClient(_emailSettings.PrimaryDomain, _emailSettings.PrimaryPort))
                {
                    smtp.Credentials = new NetworkCredential(_emailSettings.UsernameEmail,_emailSettings.UsernamePassword);
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(mail);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string createTemplateConfirmEmail(string message, string link){

            string body = 
            @"<table><tbody>
                             <tr>            
                                <td bgcolor=""#FFA73B"" align=""center"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                                    <tbody>
                                    <tr>
                                        <td align=""center"" valign=""top"" style=""padding: 40px 10px 40px 10px;""> </td>
                                    </tr>
                                    </tbody>
                                </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor=""#FFA73B"" align=""center"" style=""padding: 0px 10px 0px 10px;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                                    <tbody>
                                    <tr>
                                        <td bgcolor=""#ffffff"" align=""center"" valign=""top""
                                        style=""padding: 40px 20px 20px 20px; border-radius: 4px 4px 0px 0px; color: #111111; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 48px; font-weight: 400; letter-spacing: 4px; line-height: 48px;"">
                                        <h1 style=""font-size: 48px; font-weight: 400; margin: 2;"">Bem vindo</h1> <img
                                            src="" https://img.icons8.com/clouds/100/000000/handshake.png"" width=""125"" height=""120""
                                            style=""display: block; border: 0px;"">
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor=""#f4f4f4"" align=""center"" style=""padding: 0px 10px 0px 10px;"">
                                <table border=""0"" cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""max-width: 600px;"">
                                    <tbody>
                                    <tr>
                                        <td bgcolor=""#ffffff"" align=""left""
                                        style=""padding: 20px 30px 40px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                        <p style=""margin: 0;"">Nos estamos felizes em termos você aqui. Clique no botão abaixo para confirmar sua
                                            conta</p>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td bgcolor=""#ffffff"" align=""left"">
                                        <table width=""100%"" border=""0"" cellspacing=""0"" cellpadding=""0"">
                                            <tbody>
                                            <tr>
                                                <td bgcolor=""#ffffff"" align=""center"" style=""padding: 20px 30px 60px 30px;"">
                                                <table border=""0"" cellspacing=""0"" cellpadding=""0"">
                                                    <tbody>
                                                    <tr>
                                                        <td align=""center"" style=""border-radius: 3px;"" bgcolor=""#FFA73B""><a href=""#mylink""
                                                            target=""_blank""
                                                            style=""font-size: 20px; font-family: Helvetica, Arial, sans-serif; color: #ffffff; text-decoration: none; color: #ffffff; text-decoration: none; padding: 15px 25px; border-radius: 2px; border: 1px solid #FFA73B; display: inline-block;"">Confirmar
                                                            cadastro</a></td>
                                                    </tr>
                                                    </tbody>
                                                </table>
                                                </td>
                                            </tr>
                                            </tbody>
                                        </table>
                                        </td>
                                    </tr> <!-- COPY -->
                                    <tr>
                                        <td bgcolor=""#ffffff"" align=""left""
                                        style=""padding: 0px 30px 0px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                        </td>
                                    </tr> <!-- COPY -->
                                    <tr>
                                        <td bgcolor=""#ffffff"" align=""left""
                                        style=""padding: 20px 30px 20px 30px; color: #666666; font-family: 'Lato', Helvetica, Arial, sans-serif; font-size: 18px; font-weight: 400; line-height: 25px;"">
                                        <p> #mensagem</p>
                                        </td>
                                    </tr>
                                    </tbody>
                                </table>
                                </td>
                            </tr>
                            </tbody>
                             <table>";



                           return body.Replace("#mylink",link).Replace("#mensagem",message);
        }
    }
}