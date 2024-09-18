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
    public IActionResult GetMessagesByBoth(string senderEmail, string receiverEmail)
    {
        // Получение данных пользователя по email и передача их в представление
        var msgs = _jsonFileService.GetMsgsByBoth(senderEmail, receiverEmail);
        if (msgs == null)
        {
            return View("404");
        }
        return View("Messages", msgs);
    }
    
    [HttpGet]
    public IActionResult GetMsgBySender(string senderEmail)
    {
        // Получение данных пользователя по id и передача их в представление
        var msgs = _jsonFileService.GetMsgsBySender(senderEmail);
        if (msgs == null)
        {
            return View("404");
        }
        return View("Messages", msgs);
    }
    
    [HttpGet]
    public IActionResult GetMsgByReceiver(string receiverEmail)
    {
        // Получение данных пользователя по id и передача их в представление
        var msgs = _jsonFileService.GetMsgsByReceiver(receiverEmail);
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
        if (_jsonFileService.GetUserByEmail(message.SenderEmail) == null)
        {
            // Добавляем ошибку в ModelState
            ModelState.AddModelError("senderEmail", "Пользователя с таким email не существует.");
        }
        
        if (_jsonFileService.GetUserByEmail(message.ReceiverEmail) == null)
        {
            // Добавляем ошибку в ModelState
            ModelState.AddModelError("receiverEmail", "Пользователя с таким email не существует.");
        }
        
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