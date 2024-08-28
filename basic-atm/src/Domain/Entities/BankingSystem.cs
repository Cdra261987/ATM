using Domain.Abstractions;

namespace Domain.Entities;

public abstract class BankingSystem : IBankingSystem
{
    public UserAccount LoggedInAccount { get; protected set; }
    protected readonly List<Account> _accounts;

    protected BankingSystem(List<Account> accounts)
    {
        _accounts = accounts.Count == 0 
            ? throw new InvalidOperationException("Do not have enough accounts for ATM Access")
            : accounts;
    }

    public void Deposit(decimal amount)
    {
        LoggedInAccount.Deposit(amount);
    }

    public void Transfer(UserAccount destination, decimal amount)
    {
        LoggedInAccount.Transfer(destination, amount);
    }

    public void Withdraw(decimal amount)
    {
        LoggedInAccount.Withdraw(amount);
    }

    public decimal CheckBalance() => LoggedInAccount.CheckBalance();
}