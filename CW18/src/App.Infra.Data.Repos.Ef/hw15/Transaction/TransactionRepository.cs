using App.Domain.Core.hw15.Transaction.Data.Repositories;
using App.Domain.Core.hw15.Transaction.Dtos;
using App.Infra.Data.Db.SqlServer.Ef.Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Repos.Ef.hw15.Transaction;

public class TransactionRepository : ITransactionRepositories
{
    private readonly AppDbContext _appDbContext;

    public TransactionRepository(AppDbContext db)
    {
        _appDbContext = db;
    }
    public void Add(Domain.Core.hw15.Transaction.Entity.Transaction transaction)
    {
        _appDbContext.Transactions.Add(transaction);
        _appDbContext.SaveChanges();
    }

    public float DailyWithdrawal(string cardNumber)
    {
        var amountOfTransactions = _appDbContext.Transactions
            .Where(x => x.ActionAt.Date == DateTime.Now.Date && x.SourceCard.CardNumber == cardNumber)
            .Sum(x => x.Amount);

        return amountOfTransactions;
    }

    public List<GetTransactionsDto> GetListOfTransactions(string cardNumber)
    {
        return _appDbContext.Transactions
           .Where(x => x.SourceCard.CardNumber == cardNumber || x.DestinationCard.CardNumber == cardNumber)
           .Select(x => new GetTransactionsDto
           {
               SourceCardNumber = x.SourceCard.CardNumber,
               DestinationsCardNumber = x.DestinationCard.CardNumber,
               ActionAt = x.ActionAt,
               Amount = x.Amount,
               IsSuccess = x.IsSuccess,
           }).ToList();
    }
}
