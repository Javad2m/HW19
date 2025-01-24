using App.Domain.AppServices.hw15.Card;
using App.Domain.AppServices.hw15.Transaction;
using App.Domain.AppServices.hw15.User;
using App.Domain.Core.hw15.Card.AppServices;
using App.Domain.Core.hw15.Card.Services;
using App.Domain.Core.hw15.Result;
using App.Domain.Core.hw15.Transaction.AppServices;
using App.Domain.Core.hw15.Transaction.Services;
using App.Domain.Core.hw15.User.AppServices;
using App.Domain.Core.hw15.User.Services;
using App.Domain.Services.hw15.Card;
using App.Domain.Services.hw15.Transaction;
using App.Domain.Services.hw15.User;

ICardAppServices cardService = new CardAppServices();
ITransactionAppServices transactionService = new TransactionAppServices();
IUserAppServices userServices = new UserAppServices();

Result passwordValidationResult;
string cardNumber;

Menu();


void Menu()
{
    Console.Clear();

    Console.Write("CardNumber : ");
    cardNumber = Console.ReadLine();

    Console.Write("Password : ");
    var password = Console.ReadLine();

    passwordValidationResult = cardService.PasswordIsValid(cardNumber, password);

    Console.WriteLine(passwordValidationResult.Message);
    Console.ReadKey();
    if (passwordValidationResult.IsSuccess)
    {
        UsL();
    }


}

void UsL()
{
    try
    {
        Console.Clear();

        Console.WriteLine("1.Transfer Money");
        Console.WriteLine("2.Show Transactions");
        Console.WriteLine("3.Show Balance");
        Console.WriteLine("4.Change Password");
        Console.WriteLine("5.Log Out");

        var selectedItem = Console.ReadKey();

        Console.Clear();

        var sourceCardNumber = cardNumber;
        Console.WriteLine($"Source CardNumber Is {cardNumber} ");
        Console.WriteLine();

        switch (selectedItem.KeyChar)
        {
            case '1':
                {
                    TransferMoney(sourceCardNumber);
                    break;
                }
            case '2':
                {
                    ShowListOfTransactions();
                    break;
                }

            case '3':
                {
                    var balance = cardService.ReciveInventoryInventory(sourceCardNumber);
                    Console.WriteLine($"Your Balance: {balance}");
                    Console.ReadKey();
                    UsL();


                    break;
                }
            case '4':
                {
                    Console.WriteLine("Pls Enter The Old Password");
                    var old = Console.ReadLine();
                    Console.WriteLine("Pls Enter The New Password");
                    var nw = Console.ReadLine();
                    var res = cardService.ChPass(sourceCardNumber, old, nw);
                    if (res.IsSuccess)
                    {
                        Console.WriteLine($"{res.Message}");
                        Console.ReadKey();
                        UsL();
                    }
                    else if (!res.IsSuccess)
                    {
                        Console.WriteLine($"{res.Message}");
                        Console.ReadKey();
                        UsL();

                    }
                    break;
                }
            case '5':
                {
                    Menu();
                    break;

                }
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        UsL();
    }

}

void TransferMoney(string sourceCardNumber)
{
    try
    {
        Console.Write("Please Insert Destination CardNumber : ");
        var destinationCardNumber = Console.ReadLine();

        Console.Write("Amount : ");
        var amount = float.Parse(Console.ReadLine() ?? string.Empty);

        Console.WriteLine($"Name Of Car Is: {cardService.ShowName(sourceCardNumber)}");
        Console.WriteLine("For Accept: y Or n");
        var chr = Console.ReadLine();

        if (chr == "y")
        {
            var cd = cardService.Find(sourceCardNumber);

            userServices.GenerateVerificationCode(cd.Id, cd.HolderName);
            Console.WriteLine("Enter Verification Code: ");
            int enteredCode = int.Parse(Console.ReadLine());
            bool isCodeValid = userServices.ValidateVerificationCode(cd.Id, cd.HolderName, enteredCode);

            if (isCodeValid)
            {
                var transactionStatus = transactionService.TransferMoney(sourceCardNumber, destinationCardNumber, amount);

                Console.WriteLine(transactionStatus);

                Console.ReadKey();
                UsL();
            }

            else
            {
                Console.WriteLine("Code Not True");
                Console.ReadKey();
                UsL();
            }



        }
        else
        {
            Console.WriteLine("Return To Menu");
            Console.ReadKey();
            UsL();
        }
    }
    catch (Exception ex)
    {

        Console.WriteLine(ex.Message);
        Console.ReadKey();
        UsL();
    }
}



void ShowListOfTransactions()
{
    try
    {
        var transactions = transactionService.GetListOfTransactions(cardNumber);

        foreach (var item in transactions)
        {
            Console.WriteLine($"{item.SourceCardNumber} - {item.DestinationsCardNumber} - Amount: {item.Amount}");
        }

        Console.ReadKey();
        UsL();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
        Console.ReadKey();
        UsL();
    }

}