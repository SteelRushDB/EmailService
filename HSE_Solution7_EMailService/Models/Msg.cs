namespace HSE_Solution7_EMailService.Models;

public class Msg
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Content { get; set; }
    public int SenderId { get; set; }
    public int ReceiverId { get; set; }

    public Msg(int id, string subject, string content, int senderId, int receiverId)
    {
        Id = id;
        Subject = subject;
        Content = content;
        SenderId = senderId;
        ReceiverId = receiverId;
    }
    public Msg()
    {
        
    }
}