namespace HSE_Solution7_EMailService.Models;

public class Msg
{
    public string Subject { get; set; }
    public string Content { get; set; }
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }

    public Msg(string subject, string content, string senderEmail, string receiverEmail)
    {
        Subject = subject;
        Content = content;
        SenderEmail = senderEmail;
        ReceiverEmail = receiverEmail;
    }
    public Msg()
    {
        
    }
}