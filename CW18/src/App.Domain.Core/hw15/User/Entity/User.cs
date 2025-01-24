using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.hw15.User.Entity;
using App.Domain.Core.hw15.Card.Entity;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int? Code { get; set; }
    public List<Card> Cards { get; set; }
}
