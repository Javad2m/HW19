using App.Domain.AppServices.hw15.Transaction;
using App.Domain.Core.hw15.Transaction.AppServices;
using App.Infra.Data.Db.SqlServer.Ef.Cur;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoint.MVC.ATM.Controllers
{
    public class TransactionController : Controller
    {
        private readonly ITransactionAppServices transactionAppServices;

        public TransactionController(ITransactionAppServices transactionAppServicess)
        {
            transactionAppServices = transactionAppServicess;
        }
        public IActionResult Index()
        {
            var tr = transactionAppServices.GetListOfTransactions(Cur.CurUser.CardNumber);
            if (TempData["ErrorMessage"] != null)
            {
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
            }
            return View(tr);
        }


        public IActionResult CardToCard()
        {
            return View(Cur.CurUser);
        }

        [HttpPost]
        public IActionResult CardAction(string SourceCard,string DestinationCard, float Amount)
        {
          var mess =  transactionAppServices.TransferMoney(SourceCard, DestinationCard, Amount);
          TempData["ErrorMessage"] = mess;

            return RedirectToAction("Index");
        }
    }
}
