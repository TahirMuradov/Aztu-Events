
using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using Aztu_Events.Core.Utilities.Results.Abstract;
using Aztu_Events.Core.Utilities.Results.Concrete.SuccessResults;
using Aztu_Events.Core.Utilities.Results.Concrete.ErrorResults;
using Aztu_Events.Core.Helper.EmailHelper;
using System.Globalization;



namespace Aztu_Events.Core.Helper.EmailHelper.Concrete
{
    public class EmailHelper : IEmailHelper
    {
        private readonly IConfiguration _config;

        public EmailHelper(IConfiguration config)
        {
            _config = config;
        }

        public async Task<IResult> ApproveConfransSendEmail(string userEmail, string name, DateTime dateTime, string AuditoriumNumber, bool UpdateDate = false)
        {
            string htmlBody=string.Empty;
            if (UpdateDate)
            {
                htmlBody = $"Hörmətli {name} sizin konfransiniz vaxtı dəyişildi.Kecirilmə tarixi və mekani:<br/> tarix: {dateTime.ToString("dd MMM yyyy", new CultureInfo("az-AZ"))}<br/ Məkan:{AuditoriumNumber}<br/Diqqətiniz üçün təşəkkürlər.>>";

            }
            else
            {

            htmlBody = $"Hörmətli {name} sizin konfransiniza təsdiq gəldi.Kecirilmə tarixi və mekani:<br/> tarix: {dateTime.ToString("dd MMM yyyy", new CultureInfo("az-AZ"))}<br/ Məkan:{AuditoriumNumber}<br/Diqqətiniz üçün təşəkkürlər.>>";
            }
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailServices:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Importance = MessageImportance.High;
            email.Subject = "Aztu-Elektron Idareetme Sistemi";

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

        public async Task<IResult> DeclineConfransEmailAsync(string userEmail, string name, string responseMessage)
        {
            string htmlBody = $"Hörmətli {name} sizin konfransiniza imtina gəldi.İmtina gəlmə səbəbi:<br/> {responseMessage}";
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailServices:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Importance = MessageImportance.High;
            email.Subject = "Aztu-Elektron Idareetme Sistemi";

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


        public async Task<IResult> ApproveConfransSendEmailForGuest(string userEmail, string name, DateTime dateTime, string AuditoriumNumber,string confransDetailUrl, bool UpdateDate = false, bool SendEmailGuest = false)
        {
            string htmlBody = string.Empty;
            if (UpdateDate && SendEmailGuest)
            {
                htmlBody = $"Hörmətli {name} sizi dəvət etdiyimiz konfrasin vaxtı dəyişildi.Kecirilmə tarixi və mekani:<br/> tarix: {dateTime.ToString("dd MMM yyyy", new CultureInfo("az-AZ"))}<br/ Məkan:{AuditoriumNumber}<br/> Daha Ətraflı Kecid linki:<br/><a class='btn btn-primary' href='{confransDetailUrl}' role='button' style='display: inline-block; padding: 6px 12px; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; cursor: pointer; border: 1px solid transparent; border-radius: 4px; text-decoration: none; color: #fff; background-color: #337ab7; border-color: #2e6da4;'>Keçid Linki</a> <br/> Diqqətiniz üçün təşəkkürlər.>>";

            }
            else if(!SendEmailGuest&& !UpdateDate)
            {

                htmlBody = $"Hörmətli {name} sizi  konfrasa dəvət edirik.Kecirilmə tarixi və mekani:<br/> tarix: {dateTime.ToString("dd MMM yyyy", new CultureInfo("az-AZ"))}<br/ Məkan:{AuditoriumNumber}<br/> Daha Ətraflı Kecid linki:<br/><a class='btn btn-primary' href='{confransDetailUrl}' role='button' style='display: inline-block; padding: 6px 12px; font-size: 14px; font-weight: 400; line-height: 1.42857143; text-align: center; white-space: nowrap; vertical-align: middle; cursor: pointer; border: 1px solid transparent; border-radius: 4px; text-decoration: none; color: #fff; background-color: #337ab7; border-color: #2e6da4;'>Keçid Linki</a> <br/> Diqqətiniz üçün təşəkkürlər.>>";
            }
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config["EmailServices:FromEmail"]));
            email.To.Add(MailboxAddress.Parse(userEmail));
            email.Importance = MessageImportance.High;
            email.Subject = "Aztu-Elektron Idareetme Sistemi";

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
