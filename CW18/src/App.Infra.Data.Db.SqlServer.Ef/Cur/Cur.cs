using App.Domain.Core.hw15.Card.Entity;
using App.Domain.Core.hw15.User.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Infra.Data.Db.SqlServer.Ef.Cur
{
    public static class Cur
    {
        public static Card? CurUser { get; set; }
    }
}
