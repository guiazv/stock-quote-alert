namespace Utils
{
  public static class Mailer
  {
    private static System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Environment.GetEnvironmentVariable("SMTP_HOST"))
    {
      Port = int.TryParse(Environment.GetEnvironmentVariable("SMTP_PORT"), out var port) ? port : throw new Exception("Error creating SMTP Client: invalid port"),
      Credentials = new System.Net.NetworkCredential(Environment.GetEnvironmentVariable("SMTP_USERNAME"), Environment.GetEnvironmentVariable("SMTP_PASSWORD")),
      EnableSsl = bool.TryParse(Environment.GetEnvironmentVariable("SMTP_SSL"), out var sslEnabled) ? sslEnabled : false,
    };
    public static Task Send(System.Net.Mail.MailMessage message)
    {
      return client.SendMailAsync(message);
    }
  }
}
