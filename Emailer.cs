using System.Net.Mail;

namespace HomeIP
{
    internal class Emailer
    {

        private static string ByName(List<string> Values, string Name)
        {
            string value = Values.FirstOrDefault(s => s.StartsWith($"{Name}="));
            if (value is null) return null;
            return value.Replace($"{Name}=", "");
        }

        public static void Send(string message, List<string> Server, List<string> SendTo)
        {
            string user = ByName(Server, "user");
            string sender = ByName(Server, "sender");
            SmtpClient mySmtpClient = new SmtpClient(ByName(Server, "server"));
            mySmtpClient.Port = Convert.ToInt32(ByName(Server, "port"));
            mySmtpClient.EnableSsl = true;
            // set smtp-client with basicAuthentication
            mySmtpClient.UseDefaultCredentials = false;
            System.Net.NetworkCredential basicAuthenticationInfo = new
               System.Net.NetworkCredential(user, ByName(Server, "pass"));
            mySmtpClient.Credentials = basicAuthenticationInfo;
            // add from,to mailaddresses
            MailAddress from = new MailAddress(user, sender);
            MailMessage myMail = new System.Net.Mail.MailMessage(from, new MailAddress("null@null.com", sender));
            myMail.To.Clear();
            // add ReplyTo
            MailAddress replyTo = new MailAddress(user);
            myMail.ReplyToList.Add(replyTo);

            // set subject and encoding
            myMail.Subject = "Home IP";
            myMail.SubjectEncoding = System.Text.Encoding.UTF8;

            // set body-message and encoding
            string timenow = DateTime.Now.ToString();
            myMail.Body = $"<b>Home IP</b><br>{message}<br>Time: {timenow}";
            myMail.BodyEncoding = System.Text.Encoding.UTF8;
            // text or html
            myMail.IsBodyHtml = true;
            SendTo.ForEach(s =>
            {
                MailAddress to = new MailAddress(s, sender);
                myMail.To.Add(to);
            });
            mySmtpClient.Send(myMail);
        }

    }
}
