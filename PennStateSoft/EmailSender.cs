using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Mandrill;
using Mandrill.Model;
using PennStateSoft.Data;
using PennStateSoft;
using FluentEmail;

namespace BlazorSample.Components.Account;

public class EmailSender(IOptions<AuthMessageSenderOptions> optionsAccessor,
    ILogger<EmailSender> logger) : IEmailSender<ApplicationUser>
{
    private readonly ILogger logger = logger;
    readonly string layoutImg = "logo.png";

    public AuthMessageSenderOptions Options { get; } = optionsAccessor.Value;

    public Task SendConfirmationLinkAsync(ApplicationUser user, string email,
        string confirmationLink) => SendEmailAsync(email, "Confirm your email",
            "<!DOCTYPE html>"
            +"<html>" +
            "<style>" +
            "*{box-sizing: border-box;}" +
            "body{font-family: Arial, Helvetica, sans-serif; background-color: WhiteSmoke; padding:10px;}" + 
            "header{tbackground-color: RoyalBlue; padding:0px; text-align:center; font-size:20px; color:WhiteSmoke;}"  +
            "section{display: -webkit-flex;ndisplay: flex; }" + 
            "article{background-color:GhostWhite; -webkit-flex: 3; -ms-flex: 1; text-align:center; padding:80px;}" + 
            "footer{background-color: RoyalBlue; padding:15px; text-align:center; color:WhiteSmoke; }" +
            " @media (max-width: 600px) {section {-webkit-flex-direction: column; flex-direction: column;}" +
            "</style>" +
            "<body>" +
            "<header style=\"align:left\">" +
            "<img src=\"{layoutImg}\"" +
            "style=\"width:200px; height:70px;\">" +
            " </header>" +
            "<footer> Welcome *|UserName|*,</footer>" +
            "<section>" +
            "<article>" +
            "<p>We are glad to have you on board. Please confirm your account by<a href=\"{confirmationLink}\"> clicking here</a>. </p>" +
            "</article>" +
            "</section>" +
            "<footer>" +
            "Happy Schedule Management," +
            "</footer>" +
            "<footer>" +
            "PennStateSoft" +
            "</footer>" +
            "</body>" +
            "</html>");

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
        //MailChimp/Mandrill has suspended this mailclient (tied to my domain).
        //Need to implement a new client.
        return;
        
        int index = toEmail.IndexOf('@');
        string user = toEmail.Substring(0, index);
        var api = new MandrillApi(apiKey);
        var mandrillMessage = new MandrillMessage("noreply@PennStateSoft.com", toEmail,
            subject, message);
        mandrillMessage.MergeLanguage = new MandrillMessageMergeLanguage();
        mandrillMessage.AddGlobalMergeVars("UserName", user);
        await api.Messages.SendAsync(mandrillMessage);

        logger.LogInformation("Email to {EmailAddress} sent!", toEmail);
        
    }
}