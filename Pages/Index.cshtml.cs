using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MailKit.Net.Smtp;
using MimeKit;

public class IndexModel : PageModel
{
    [BindProperty]
    public string SmtpServer { get; set; }
    [BindProperty]
    public int SmtpPort { get; set; }
    [BindProperty]
    public string FromEmail { get; set; }
    [BindProperty]
    public string ToEmail { get; set; }
    [BindProperty]
    public string Username { get; set; }
    [BindProperty]
    public string Password { get; set; }
    public bool EmailSent { get; set; }
    public string ErrorMessage { get; set; }

    public string Body = "this is a test message from SMTP test tool";
    public string Subject = "SMTP TEST";
    public void OnPost()
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("", FromEmail));
            message.To.Add(new MailboxAddress("", ToEmail));
            message.Subject = Subject;
            message.Body = new TextPart("plain")
            {
                Text = Body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(SmtpServer, SmtpPort, true);
                client.Authenticate(Username, Password);
                client.Send(message);
                client.Disconnect(true);
            }

            EmailSent = true;
        }
        catch (Exception ex)
        {
            ErrorMessage = ex.Message;
        }
    }
}
