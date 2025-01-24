using App.Domain.Core.hw15.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.Transaction.Data.Repositories;

public interface ITransactionRepositories
{
    List<GetTransactionsDto> GetListOfTransactions(string cardNumber);
    float DailyWithdrawal(string cardNumber);
    void Add(Entity.Transaction transaction);
}
