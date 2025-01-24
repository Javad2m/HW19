using App.Domain.Core.hw15.Card.AppServices;
using App.Domain.Core.hw15.Card.Services;
using App.Domain.Core.hw15.Result;
using App.Domain.Services.hw15.Card;
using App.Infra.Data.Repos.Ef.hw15.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.hw15.Card;
using App.Domain.Core.hw15.Card.Entity;
using App.Infra.Data.Db.SqlServer.Ef.Cur;

public class CardAppServices : ICardAppServices
{
    private readonly ICardServices _cardServices;
    public CardAppServices()
    {
        _cardServices = new CardServices();
    }


    public Result PasswordIsValid(string cardNumber, string password)
    {
        var tryCount = _cardServices.GetWrongPasswordTry(cardNumber);

        if (tryCount > 3)
            return new Result() { IsSuccess = false, Message = "You have entered the wrong password 3 times. Your account is permanently blocked" };

        var passwordIsValid = _cardServices.Login(cardNumber, password);

        if (passwordIsValid == false)
        {
            _cardServices.SetWrongPasswordTry(cardNumber);
            return new Result() { IsSuccess = false, Message = "Card number Or Password Is Wrong" };
        }
        else
        {
            _cardServices.ClearWrongPasswordTry(cardNumber);
            var ttt = Find(cardNumber);

            Cur.CurUser = ttt;
            return new Result() { IsSuccess = true, Message = "Welcome" };
        }
    }

    public float ReciveInventoryInventory(string cardNumber)
    {
        var balance = _cardServices.Inventory(cardNumber);
        return balance;
    }

    public string ShowName(string cardNumber)
    {
        var card = _cardServices.GetCardBy(cardNumber);
        return card.HolderName;
    }

    public Result ChPass(string cardNumber, string oldPass, string newPass)
    {
        if (newPass.Length < 4)
        {
            return new Result() { IsSuccess = false, Message = "Password Most be =+4 Char" };

        }


        var card = _cardServices.GetCardBy(cardNumber);
        if (card.Password == oldPass)
        {
            _cardServices.ChangePassword(cardNumber, oldPass, newPass);
            return new Result() { IsSuccess = true, Message = "Password Is Changed" };
        }
        else
        {
            return new Result() { IsSuccess = false, Message = "Password Is Changed Wrong!" };
        }
    }

    public Card Find(string cardNumber)
    {
        var card = _cardServices.GetCardBy(cardNumber);
        return card;
    }
}
