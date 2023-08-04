using MailKit.Net.Smtp;
using MimeKit;

namespace BankConsole;

public static class EmailService
{

    public static void SendEmail()
    {   
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress ("Felipe Mata", "fmrivera1309@gmail.com"));
        message.To.Add(new MailboxAddress ("Admin", "felipe_motoss@hotmail.com"));
        message.Subject = "BankConsole: Usuarios nuevos";

        message.Body = new TextPart("plain"){
            Text = GetEmailText()
        };

        using (var client = new SmtpClient ()){
            client.Connect("smtp.gmail.com", 587, false);
            client.Authenticate("fmrivera1309@gmail.com", "1231212");
            client.Send(message);
            client.Disconnect(true);
        }


    }

    private static string GetEmailText()
    {
        List<User> newUsers = Storage.GetNewUsers();

        if(newUsers.Count == 0)
            return "No hay usuarios nuevos";
        
        string emailText = "Usuarios agregados hoy:\n";

        foreach (User user in newUsers)
        {
            emailText += "\t+ " + user.showData() + "\n";
        }

        return emailText;
    }


}