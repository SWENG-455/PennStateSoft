using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PennStateSoft;
using MimeKit;
using PennStateSoft.Data.Models;

namespace BlazorSample.Components.Account;

public class EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
    ILogger<EmailSender> logger) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;

    public AuthMessageSenderOptions Options { get; } = optionsAccessor.Value;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirm your email", 
            $"Please confirm your account by clicking here: {confirmationLink}" +
            "\n\nHappy Schedule Management," +
            "\nPennStateSoft");

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email,
        string resetLink) => SendEmailAsync(email, "Reset your password",
        $"Looks like you're having trouble accessing your account. " +
            $"Please reset your password by <a href='{resetLink}'>clicking here</a>.");

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email,
        string resetCode) => SendEmailAsync(email, "Reset your password",
        $"Looks like you're having trouble accessing your account. " +
            $"Please reset your password using the following code: {resetCode}");

    public async Task SendEmailAsync(string toEmail, string subject, string message)
    {
        if (string.IsNullOrEmpty(Options.EmailAuthKey))
        {
            throw new Exception("Null EmailAuthKey");
        }

        await Execute(Options.EmailAuthKey, subject, message, toEmail);
    }

    public async Task Execute(string apiKey, string subject, string message,
        string toEmail)
    {   
        int index = toEmail.IndexOf('@');
        string user = toEmail.Substring(0, index);

        var msg = new MimeMessage();
        var builder = new BodyBuilder();

        msg.From.Add(InternetAddress.Parse("psuarchivereset@gmail.com"));
        msg.To.Add(InternetAddress.Parse(toEmail));
        msg.Subject = subject;

        string body = message;
        string greeting = "Hello " + user + ",\n\n";
        message = greeting + body;

        builder.TextBody = message;

        msg.Body = builder.ToMessageBody();

        using (var client = new MailKit.Net.Smtp.SmtpClient())
        {
            client.ServerCertificateValidationCallback = (s, certificate, chain, sslPolicyErrors) => true;

            await client.ConnectAsync("smtp.gmail.com", 587, false);
            await client.AuthenticateAsync("psuarchivereset@gmail.com", GlobalVariables.Password);

            await client.SendAsync(msg);
            await client.DisconnectAsync(true);
            logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
        }
        /*
        var options = new RestClientOptions("https://api.mailgun.net/v3")
        {
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        };
        var client = new RestClient(options);
        var request = new RestRequest();
        request.AddParameter("domain", "sandboxcec030827ba94d4dbb2989a51345f1e8.mailgun.org", ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from", "Excited User <mailgun@sandboxcec030827ba94d4dbb2989a51345f1e8.mailgun.org>");
        request.AddParameter("to", toEmail);
        request.AddParameter("subject", subject);
        request.AddParameter("html", message);
        request.Method = Method.Post;
        var response = await client.ExecuteAsync(request);

        if (response.IsSuccessStatusCode)
        {
            logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
        }
        */
    }
}

internal class GlobalVariables
{
    public static string Password = "xnnb jtri wzag vkll ";
    public static string FromEmail = "psuarchivereset@gmail.com";
}