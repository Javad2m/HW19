using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.Card.AppServices;

using App.Domain.Core.hw15.Card.Entity;
using App.Domain.Core.hw15.Result;

public interface ICardAppServices
{
    public Result PasswordIsValid(string cardNumber, string password);
    public float ReciveInventoryInventory(string cardNumber);

    public string ShowName(string cardNumber);

    public Result ChPass(string cardNumber, string oldPass, string newPass);
    public Card Find(string cardNumber);
}
