using ATM.Domain.Exceptions;
using Domain.Abstractions;

namespace Domain.Entities;

public enum Gender
{
    Male,
    Female,
    Other
}

public class UserAccount : Account, IUserOperation, IUserInteraction
{
    public UserAccount(string username, string password, string idCard, string pin, decimal balance) 
        : base(idCard, pin, balance)
    {
        Username = username;
        Password = password;
    }
    
    public UserAccount(string username, string password) 
        : base(username, password)
    {
    }

    
    public void Deposit(decimal amount)
    {
        if (amount < 0)
            throw new InvalidTransactionException("Amount cannot be negative when making a deposit!");

        Balance += amount;
        AccountOperations.Add(new DepositOperation(this, amount));
    }

    public void Transfer(Account destination, decimal amount)
    {
        throw new NotImplementedException();
    }

    public void Transfer(UserAccount destination, decimal amount)
    {
        if (destination is null)
        {
            throw new InvalidTransactionException("Destination account cannot be null!");
        }
        destination.Deposit(amount);
        AccountOperations.Add(new TransferOperation(this, destination, amount));
    }

    public void Withdraw(decimal amount)
    {
        if (amount < 0)
            throw new InvalidTransactionException("Amount to withdraw cannot be negative!");

        if (amount > Balance)
            throw new InvalidTransactionException("Sorry, the amount to withdraw cannot be higher than your balance!");

        Balance -= amount;
        AccountOperations.Add(new WithdrawOperation(this, amount));
    }

    public decimal CheckBalance()
    {
        AccountOperations.Add(new CheckBalanceOperation(this));
        return Balance;
    }

    public bool Validate(string secret) => Password == secret;
    public int IncrementFailedAttempts() => LoginAttempts--;
    public int ResetFailedAttempts() => LoginAttempts = 3;
    public void Unlock() => Locked = false;
    public void Lock() => Locked = true;
}


// public class User
// {
//     public string FirstName { get; private set; }
//     public string LastName { get; private set; }
//     public Gender Gender { get; private set; }
//     public List<Account> Accounts { get; private set; }
//
//     public User(string firstName, string lastName, Gender gender)
//     {
//         FirstName = string.IsNullOrWhiteSpace(firstName) 
//             ? throw new InvalidOperationException("User cannot have null or empty first name") 
//             : firstName;
//         LastName = string.IsNullOrWhiteSpace(lastName) 
//             ? throw new InvalidOperationException("User cannot have null or empty last name") 
//             : lastName;
//         Gender = gender;
//         Accounts = new List<Account>();
//     }
//     
// }