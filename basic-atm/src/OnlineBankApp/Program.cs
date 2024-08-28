// See https://aka.ms/new-console-template for more information

using System.Text;
using Domain.Abstractions;
using Domain.Entities;

var accounts = new List<Account> {
    new UserAccount("amateus@gmail.com", "asedbasbgdaus","12345", "1111", 0),
    new UserAccount("test@gmail.com", "sh62","22222", "0000", 10000),
    new UserAccount("nunorebelo@gmail.com", "jashd","30450", "1111", 10),
    new Admin("admin@gmail.com", "1234")
};

var onlineBankingSystem = new OnlineBankingSystem(accounts);

var atmRunning = true;
while (atmRunning)
{
    Console.WriteLine(ShowInitialOperations());

    try
    {
        var operation = Console.ReadLine();

        if (operation[0] != '1' && operation[0] != '0')
            throw new InvalidOperationException("Invalid input for menu");

        if (operation[0] == '1')
        {
            var loginStatus = false;
            while (!loginStatus)
            {
                Console.Write("Username: ");
                var username = Console.ReadLine();
                Console.Write("Password: ");
                var password = Console.ReadLine();
                var account = new UserAccount(username, password);
                (loginStatus, var attempts) = onlineBankingSystem.Login(account);
            }
            
            Console.WriteLine("Login Successful");

            while (loginStatus)
            {
                Console.WriteLine(ShowAccountOperations());
            
                var op2 = Console.ReadLine();
                var selectedOperation = ValidateMenuOperation(op2[0]);
                PerformOnlineOperation(selectedOperation, onlineBankingSystem);
            }
            // TODO: Implement account operations menu
            Console.ReadLine();
        }
        else
        {
            Console.WriteLine("Exiting ATM");
        }
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

void PerformOnlineOperation(int opSelected, IBankingSystem system)
{

    if (opSelected == 1)
    {
        Console.WriteLine($"Current Balance: {onlineBankingSystem.CheckBalance()}");
    }
    else if (opSelected == 2)
    {
        Console.WriteLine($"Current Balance: {onlineBankingSystem.CheckBalance()}");
        Console.Write("Amount to deposit: ");
        var depositAmountStr = Console.ReadLine();

        bool amountIsValid = false;
        decimal depositAmountConverted = -1;
        while (!amountIsValid)
        {
            try
            {
                depositAmountConverted = decimal.Parse(depositAmountStr);
                amountIsValid = true;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Not a valid amount! Reason: {e.Message}");
            }
        }
        onlineBankingSystem.Deposit(depositAmountConverted);
        Console.WriteLine($"Amount after deposit {onlineBankingSystem.CheckBalance()}");
    }
    else if (opSelected == 3)
    {
        
    }
    else if (opSelected == 4)
    {
        
    }
    else
    {
        return;
    }
};

int ValidateMenuOperation(char opSelected) => opSelected switch
{
    '1' => 1,
    '2' => 2,
    '3' => 3,
    '4' => 4,
    '0' => 0,
    _ => throw new ArgumentOutOfRangeException(nameof(opSelected), opSelected, null)
};

string ShowInitialOperations()
{
    var sb = new StringBuilder();

    sb.AppendLine("1. Login");
    sb.AppendLine("0. Turn-off");
    sb.AppendLine("Operation. ");
    return sb.ToString();
};

string ShowAccountOperations()
{
    var sb = new StringBuilder();

    sb.AppendLine("1. Check Balance");
    sb.AppendLine("2. Deposit");
    sb.AppendLine("3. Withdraw");
    sb.AppendLine("4. Transfer");
    sb.AppendLine("0. Turn-off");
    sb.AppendLine("Operation. ");
    return sb.ToString();
};