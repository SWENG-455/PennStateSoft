using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using PennStateSoft.Data;
using PennStateSoft;
using RestSharp;
using RestSharp.Authenticators;
using Humanizer;

namespace BlazorSample.Components.Account;

public class EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
    ILogger<EmailSender> logger) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;
    readonly string layoutImg = "logo.png";

    public AuthMessageSenderOptions Options { get; } = optionsAccessor.Value;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirm email",
          "Please confirm your account by<a href=\"{confirmationLink}\"> clicking here</a>." +
            "\nHappy Schedule Management," +
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
    }
}