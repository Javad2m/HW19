using App.Domain.AppServices.hw15.Card;
using App.Domain.Core.hw15.Card.AppServices;
using App.Domain.Core.hw15.User.Services;
using App.Infra.Data.Db.SqlServer.Ef.Cur;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ConstrainedExecution;

namespace App.EndPoint.MVC.ATM.Controllers;

public class CardController : Controller
{
    private readonly ICardAppServices cardAppServices;

    public CardController(ICardAppServices cardAppServicess)
    {
        cardAppServices = cardAppServicess;
    }
    [HttpGet]
    public IActionResult Login()
    {
        if (TempData["ErrorMessage"] != null)
        {
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
        }
        return View();
    }



    [HttpPost]
    public IActionResult LoginCard(string CardNumber, string Password)
    {
        var check = cardAppServices.PasswordIsValid(CardNumber, Password);

        if (check.IsSuccess)
        {
            return RedirectToAction("Index", "Transaction");
        }
        else
        {

            TempData["ErrorMessage"] = "رمز عبور یا شماره کارت صحیح نیست.";
            return RedirectToAction("Login");
        }
    }


    [HttpGet]
    public IActionResult LogOut()
    {
        Cur.CurUser = null;
        return RedirectToAction("Login");
    }

    [HttpGet]
    public IActionResult ChangPassword()
    {

        return View(Cur.CurUser);
    }

    [HttpPost]
    public IActionResult ChangPass(string cardNumber, string oldPass, string newPass)
    {
        var res = cardAppServices.ChPass(cardNumber, oldPass, newPass);

        if (res.IsSuccess)
        {
            TempData["ErrorMessage"] = res.Message;
            return RedirectToAction("Index", "Transaction");

        }
        else
        {
            TempData["ErrorMessage"] = res.Message;
            return RedirectToAction("ChangPassword");

        }

    }
}
