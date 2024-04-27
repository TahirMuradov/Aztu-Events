
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Helper.EmailHelper;



namespace Aztu_Events.Core.Helper.EmailHelper.Concrete
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _config;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IResult> ApproveConfransSendEmail(string email, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<IResult> SendEmailAsync(string userEmail, string confirmationLink, string UserName)
        {
            string htmlBody = $"<a class='btn btn-primary' href='{confirmationLink}' role='button' style='display: inline-block; padding: 6px 12px; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; cursor: pointer; border: 1px solid transparent; border-radius: 4px; text-decoration: none; color: #fff; background-color: #337ab7; border-color: #2e6da4;'>Keçid Linki</a>";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailServices:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Importance = MessageImportance.High;
            email.Subject = "Elektron Poct Tesdiqleme";

            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = htmlBody };


            try
            {
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_config["EmailServices:ServiceName"], Convert.ToInt32(_config["EmailServices:ServicePort"]), MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_config["EmailServices:FromEmail"], _config["EmailServices:FromEmailPassword"]);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return new SuccessResult("Email Gonderilirdi");
            }
            catch (Exception ex)
            {
                return new ErrorResult(ex.Message);

            }

        }
    }
}
