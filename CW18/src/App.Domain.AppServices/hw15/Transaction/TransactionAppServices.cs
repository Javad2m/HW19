using App.Domain.Core.hw15.Card.Services;
using App.Domain.Core.hw15.Transaction.AppServices;
using App.Domain.Core.hw15.Transaction.Dtos;
using App.Domain.Core.hw15.Transaction.Services;
using App.Domain.Services.hw15.Card;
using App.Domain.Services.hw15.Transaction;
using App.Infra.Data.Repos.Ef.hw15.Card;
using App.Infra.Data.Repos.Ef.hw15.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.AppServices.hw15.Transaction;
using App.Domain.Core.hw15.Transaction.Entity;

public class TransactionAppServices : ITransactionAppServices
{
    private readonly ITransactionServices _transactionServices;
    private readonly ICardServices _cardServices;
    public TransactionAppServices(ITransactionServices transactionServices, ICardServices cardServices)
    {
        _cardServices = cardServices;
        _transactionServices = transactionServices;
    }

    public List<GetTransactionsDto> GetListOfTransactions(string cardNumber)
    {
        var list = _transactionServices.GetListOfTransactions(cardNumber);
        return list;
    }

    public string TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount)
    {
        var isSuccess = false;

        if (amount == 0)
            return "The transfer amount must be greater than 0";

        if (sourceCardNumber.Length < 16 || sourceCardNumber.Length > 16)
            return "sourceCardNumber is not valid";

        if (destinationCardNumber.Length < 16 || destinationCardNumber.Length > 16)
            return "sourceCardNumber is not valid";

        if (!_cardServices.CardIsActive(sourceCardNumber))
            return "sourceCardNumber is not valid";

        if (!_cardServices.CardIsActive(destinationCardNumber))
            return "destinationCardNumber is not valid";


        var sourceCard = _cardServices.GetCardBy(sourceCardNumber);
        var destinationCard = _cardServices.GetCardBy(destinationCardNumber);

        if (sourceCard.Balance < amount)
            return "your card doesn't have enough balance for this transaction";

        if (amount > 1000 && sourceCard.Balance < amount * (float)1.015)
            return "your card doesn't have enough balance for this transaction";

        if (amount < 1000 && sourceCard.Balance < amount * (float)1.005)
            return "your card doesn't have enough balance for this transaction";

        if ((_transactionServices.DailyWithdrawal(sourceCardNumber) + amount) > 250)
            return "Your daily transfer limit is full";

        try
        {

            _cardServices.Deposit(destinationCardNumber, amount);
            if (amount > 1000)
            {
                amount = amount * (float)1.015;
            }
            else if (amount < 1000)
            {
                amount = amount * (float)1.005;
            }
            _cardServices.Withdraw(sourceCardNumber, amount);

            isSuccess = true;

        }
        catch (Exception e)
        {
            isSuccess = false;
            return "Transfer money failed";
        }
        finally
        {
            var transaction = new Transaction()
            {
                SourceCardId = sourceCard.Id,
                DestinationCardId = destinationCard.Id,
                Amount = amount,
                ActionAt = DateTime.Now,
                IsSuccess = isSuccess
            };

            _transactionServices.Add(transaction);
        }
        return "The money transfer operation was successful";
    }
}
