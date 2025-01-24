using App.Domain.Core.hw15.Transaction.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.Transaction.AppServices
{
    public interface ITransactionAppServices
    {
        public string TransferMoney(string sourceCardNumber, string destinationCardNumber, float amount);
        public List<GetTransactionsDto> GetListOfTransactions(string cardNumber);
    }
}
