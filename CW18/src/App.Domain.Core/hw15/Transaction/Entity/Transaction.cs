using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.Transaction.Entity;
using App.Domain.Core.hw15.Card.Entity;

public class Transaction
{
    public int Id { get; set; }
    public int SourceCardId { get; set; }
    public int DestinationCardId { get; set; }
    public DateTime ActionAt { get; set; }
    public float Amount { get; set; }
    public bool IsSuccess { get; set; }

    public Card SourceCard { get; set; }
    public Card DestinationCard { get; set; }
}
