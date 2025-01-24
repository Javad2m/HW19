using App.Domain.Core.hw15.Card.Data.Repositories;
using App.Infra.Data.Db.SqlServer.Ef.Dal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.hw15.Card
{
    public class CardRepository : ICardRepositories
    {

        private readonly AppDbContext _appContext;
        public CardRepository()
        {
            _appContext = new AppDbContext();
        }
        public bool CardIsActive(string cardNumber)
        {
            var isEx = _appContext.Cards.Any(x => x.CardNumber == cardNumber && x.IsActive);
            return isEx;
        }

        public void ChangePassword(string cardNumber, string oldPass, string newPass)
        {
            var card = _appContext.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);
            card.Password = newPass;
            _appContext.SaveChanges();
        }

        public void ClearWrongPasswordTry(string cardNumber)
        {
            var card = _appContext.Cards
         .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                throw new Exception($"cannot found card with number {cardNumber}");
            }

            card.WrongPasswordTries = 0;
            _appContext.SaveChanges();
        }

        public void Deposit(string cardNumber, float balance)
        {
            var card = _appContext.Cards
              .FirstOrDefault(x => x.CardNumber == cardNumber);


            if (card is null)
            {
                throw new Exception($"cannot found card with number {cardNumber}");
            }

            card.Balance += balance;
            _appContext.SaveChanges();
        }

        public Domain.Core.hw15.Card.Entity.Card GetCardBy(string cardNumber)
        {
            var card = _appContext.Cards.FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                throw new Exception($"Card with number {cardNumber} not found");
            }
            else
            {
                return card;
            }
        }

        public int GetWrongPasswordTry(string cardNumber)
        {
            var card = _appContext.Cards
         .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                throw new Exception($"cannot found card with number {cardNumber}");
            }

            return card.WrongPasswordTries;
        }

        public float Inventory(string cardNumber)
        {
            var card = _appContext.Cards.AsNoTracking().FirstOrDefault(x => x.CardNumber == cardNumber);
            return card.Balance;
        }

        public bool Login(string carnumber, string password)
        {
            var isTrue = _appContext.Cards.Where(l => l.CardNumber == carnumber && l.Password == password).AsNoTracking().Any();
            return isTrue;
        }

        public void SetWrongPasswordTry(string cardNumber)
        {
            var card = _appContext.Cards
         .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                throw new Exception($"cannot found card with number {cardNumber}");
            }

            card.WrongPasswordTries++;
            _appContext.SaveChanges();
        }

        public void Withdraw(string cardNumber, float balance)
        {
            var card = _appContext.Cards
               .FirstOrDefault(x => x.CardNumber == cardNumber);

            if (card is null)
            {
                throw new Exception($"cannot found card with number {cardNumber}");
            }

            card.Balance -= balance;
            _appContext.SaveChanges();
        }
    }
}
