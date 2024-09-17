using System.Text.Json;
using HSE_Solution7_EMailService.Models;

namespace HSE_Solution7_EMailService.Controllers;

public class JsonFileService
{
    private readonly string _usersFilePath = "users.json";
    private readonly string _messagesFilePath = "messages.json";
    
    private int _currentMaxId;
    private List<User> _users;
    
    public JsonFileService()
    {
        _users = ReadUsersFromFile();
        _currentMaxId = _users.Count > 0 ? _users.Max(u => u.Id) : 0;
    }
    
    // Чтение списка пользователей из JSON-файла
    public List<User> ReadUsersFromFile()
    {
        if (!File.Exists(_usersFilePath))
        {
            return new List<User>();
        }

        var json = File.ReadAllText(_usersFilePath);
        return JsonSerializer.Deserialize<List<User>>(json) ?? new List<User>();
    }

    // Запись списка пользователей в JSON-файл
    public void WriteUsersToFile(List<User> users)
    {
        var sortedUsers = users.OrderBy(user => user.Email).ToList();
        var json = JsonSerializer.Serialize(sortedUsers);
        File.WriteAllText(_usersFilePath, json);
    }

    // Инициализация списка пользователей случайными данными
    public List<User> GenerateRandomUsers(int count)
    {
        _currentMaxId = 0;
        var users = new List<User>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            users.Add(CreateUser(
                $"User {random.Next(1000, 9999)}",
                $"User{_currentMaxId + 1}@example.com"
            ));
        }
        return users;
    }

    public User CreateUser(string userName, string email)
    {
        return new User(
            ++_currentMaxId,
            userName,
            email
        );
    }
    
    public User GetUserByEmail(string email)
    {
        List<User> users = ReadUsersFromFile();
        User user = users.FirstOrDefault(i => i.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        return user;
    }
    
    public User GetUserById(int id)
    {
        List<User> users = ReadUsersFromFile();
        User user = users.FirstOrDefault(i => i.Id == id);
        return user;
    }
    
    
    
    
    
    
    
    // Чтение списка сообщений из JSON-файла
    public List<Msg> ReadMessagesFromFile()
    {
        if (!File.Exists(_messagesFilePath))
        {
            return new List<Msg>();
        }

        var json = File.ReadAllText(_messagesFilePath);
        return JsonSerializer.Deserialize<List<Msg>>(json) ?? new List<Msg>();
    }

    // Запись списка сообщений в JSON-файл
    public void WriteMessagesToFile(List<Msg> messages)
    {
        var json = JsonSerializer.Serialize(messages);
        File.WriteAllText(_messagesFilePath, json);
    }
    
    // Инициализация списка сообщений случайными данными
    public List<Msg> GenerateRandomMessages(int count, List<User> users)
    {
        var messages = new List<Msg>();
        var random = new Random();
        for (int i = 0; i < count; i++)
        {
            var senderIndex = random.Next(users.Count);
            var receiverIndex = random.Next(users.Count);
            while (receiverIndex == senderIndex)
            {
                receiverIndex = random.Next(users.Count);
            }

            messages.Add(new Msg(
                i + 1,
                $"Subject {random.Next(1, 100)}",
                $"Message content {random.Next(1, 1000)}",
                users[senderIndex].Id,
                users[receiverIndex].Id
            ));
        }
        return messages;
    }

    public List<Msg> GetMsgsByBoth(int senderId, int receiverId)
    {
        List <Msg> msgs = ReadMessagesFromFile();
        return msgs.Where(i => i.SenderId == senderId && i.ReceiverId == receiverId).ToList();
    }

    public List<Msg> GetMsgsBySender(int senderId)
    {
        List <Msg> msgs = ReadMessagesFromFile();
        return msgs.Where(i => i.SenderId == senderId).ToList();
    }
    
    public List<Msg> GetMsgsByReceiver(int receiverId)
    {
        List <Msg> msgs = ReadMessagesFromFile();
        return msgs.Where(i => i.ReceiverId == receiverId).ToList();
    }
}