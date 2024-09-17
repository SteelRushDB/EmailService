using Microsoft.AspNetCore.Mvc;

namespace HSE_Solution7_EMailService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InitController : Controller
{
    private readonly JsonFileService _jsonFileService;

    public InitController(JsonFileService jsonFileService)
    {
        _jsonFileService = jsonFileService;
    }

    // POST: api/init
    [HttpPost]
    public IActionResult InitializeData(int userCount = 10, int messageCount = 20)
    {
        // Генерация случайных пользователей
        var users = _jsonFileService.GenerateRandomUsers(userCount);
        _jsonFileService.WriteUsersToFile(users);

        // Генерация случайных сообщений
        var messages = _jsonFileService.GenerateRandomMessages(messageCount, users);
        _jsonFileService.WriteMessagesToFile(messages);

        return Ok(new { UsersGenerated = users.Count, MessagesGenerated = messages.Count });
    }
}