using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.Transaction.Dtos;

public class GetTransactionsDto
{
    public string SourceCardNumber { get; set; }
    public string DestinationsCardNumber { get; set; }
    public DateTime ActionAt { get; set; }
    public float Amount { get; set; }
    public bool IsSuccess { get; set; }
}
