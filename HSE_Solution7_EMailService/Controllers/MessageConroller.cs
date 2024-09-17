using HSE_Solution7_EMailService.Models;
using Microsoft.AspNetCore.Mvc;

namespace HSE_Solution7_EMailService.Controllers;

public class MessageController : Controller
{
    private readonly JsonFileService _jsonFileService;

    public MessageController()
    {
        _jsonFileService = new JsonFileService();
    }

    public IActionResult Index()
    {
        var messages = _jsonFileService.ReadMessagesFromFile();
        return View(messages);
    }

    [HttpGet]
    public IActionResult GetMessagesByBoth(int senderId, int receiverId)
    {
        // Получение данных пользователя по id и передача их в представление
        var msgs = _jsonFileService.GetMsgsByBoth(senderId, receiverId);
        if (msgs == null)
        {
            return View("404");
        }
        return View("Messages", msgs);
    }
    
    [HttpGet]
    public IActionResult GetMsgBySender(int senderId)
    {
        // Получение данных пользователя по id и передача их в представление
        var msgs = _jsonFileService.GetMsgsBySender(senderId);
        if (msgs == null)
        {
            return View("404");
        }
        return View("Messages", msgs);
    }
    
    [HttpGet]
    public IActionResult GetMsgByReceiver(int receiverId)
    {
        // Получение данных пользователя по id и передача их в представление
        var msgs = _jsonFileService.GetMsgsByReceiver(receiverId);
        if (msgs == null)
        {
            return View("404");
        }
        return View("Messages", msgs);
    }
    
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Create(Msg message)
    {
        if (ModelState.IsValid)
        {
            var messages = _jsonFileService.ReadMessagesFromFile();
            messages.Add(message);
            _jsonFileService.WriteMessagesToFile(messages);
            return RedirectToAction(nameof(Index));
        }
        return View(message);
    }
}