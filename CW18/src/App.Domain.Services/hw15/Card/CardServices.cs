using App.Domain.Core.hw15.Card.Data.Repositories;
using App.Domain.Core.hw15.Card.Services;
using App.Infra.Data.Repos.Ef.hw15.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.hw15.Card;

public class CardServices : ICardServices
{
    private readonly ICardRepositories cardRepositories;
    public CardServices(ICardRepositories ccardRepositories)
    {
        cardRepositories = ccardRepositories;
    }
    public bool CardIsActive(string cardNumber)
    {
        return cardRepositories.CardIsActive(cardNumber);
    }

    public void ChangePassword(string cardNumber, string oldPass, string newPass)
    {
        cardRepositories.ChangePassword(cardNumber, oldPass, newPass);
    }

    public void ClearWrongPasswordTry(string cardNumber)
    {
        cardRepositories.ClearWrongPasswordTry(cardNumber);
    }

    public void Deposit(string cardNumber, float balance)
    {
        cardRepositories.Deposit(cardNumber, balance);
    }

    public Core.hw15.Card.Entity.Card GetCardBy(string cardNumber)
    {
        return cardRepositories.GetCardBy(cardNumber);
    }

    public int GetWrongPasswordTry(string cardNumber)
    {
        return cardRepositories.GetWrongPasswordTry(cardNumber);
    }

    public float Inventory(string cardNumber)
    {
        return cardRepositories.Inventory(cardNumber);
    }

    public bool Login(string carnumber, string password)
    {
        return cardRepositories.Login(carnumber, password);
    }

    public void SetWrongPasswordTry(string cardNumber)
    {
        cardRepositories.SetWrongPasswordTry(cardNumber);
    }

    public void Withdraw(string cardNumber, float balance)
    {
        cardRepositories.Withdraw(cardNumber, balance);
    }
}
