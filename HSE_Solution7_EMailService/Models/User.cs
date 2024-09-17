namespace HSE_Solution7_EMailService.Models;

public class User
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    
    public User(int id, string userName, string email)
    {
        Id = id;
        Email = email;
        UserName = userName;
    }

    public User()
    {
        
    }
}