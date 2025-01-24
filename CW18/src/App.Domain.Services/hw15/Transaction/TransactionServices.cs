using App.Domain.Core.hw15.Transaction.Data.Repositories;
using App.Domain.Core.hw15.Transaction.Dtos;
using App.Domain.Core.hw15.Transaction.Services;
using App.Infra.Data.Repos.Ef.hw15.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Services.hw15.Transaction
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepositories transactionRepositories;
        public TransactionServices()
        {
            transactionRepositories = new TransactionRepository();
        }

        public void Add(Core.hw15.Transaction.Entity.Transaction transaction)
        {
            transactionRepositories.Add(transaction);
        }

        public float DailyWithdrawal(string cardNumber)
        {
            return transactionRepositories.DailyWithdrawal(cardNumber);
        }

        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber)
        {
            return transactionRepositories.GetListOfTransactions(cardNumber);
        }
    }
}
