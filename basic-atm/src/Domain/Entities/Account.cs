using ATM.Domain.Exceptions;

namespace Domain.Entities;

public abstract class Account
{
    public List<Card> AvailableCards; 
    public decimal Balance { get;  set; }
    public string Password { get; protected set; }
    public string Username { get; protected set; }
    public int LoginAttempts { get; protected set; } = 3;
    public bool Locked { get; protected set; } = false;

    protected List<AccountOperation> AccountOperations { get; set; }

    protected Account(string idCard, string pin, decimal balance)
    {
        Balance = balance < 0 
            ? throw new InvalidAccountException("Cannot create account with negative balance")
            : balance;
        AccountOperations = new();
        AvailableCards = new List<Card>()
        {
            new(idCard, pin)
        };
    }
    
    protected Account(string username, string password)
    {
        Username = username;
        Password = password;
    }
    

    public bool Unlock(bool isAdmin)
    {
        if (isAdmin)
        {
            Locked = false;
            return true;
        }

        return false;
    }
}