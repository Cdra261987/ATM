namespace Domain.Entities;

/// <summary>
/// Login
/// Transfer
/// Deposit
/// Withdraw
/// CheckBalance
/// </summary>
public abstract class AccountOperation
{
    public UserAccount Source { get; set; }
    public decimal CurrentBalance { get; set; }

    protected AccountOperation(UserAccount source, decimal currentBalance)
    {
        Source = source;
        CurrentBalance = currentBalance;
    }
}

public class LoginOperation(UserAccount source, Card card) : AccountOperation(source, source.Balance)
{
    public Card LoggedInCard { get; set; } = card;
}

public class DepositOperation(UserAccount source, decimal amount) : AccountOperation(source, source.Balance)
{
    public decimal Amount { get; private set; } = amount;
}

public class WithdrawOperation(UserAccount source, decimal amount) : AccountOperation(source, source.Balance)
{
    public decimal Amount { get; private set; } = amount;
}

public class TransferOperation(UserAccount source, UserAccount destination, decimal amount) 
    : AccountOperation(source, source.Balance)
{
    public decimal Amount { get; private set; } = amount;
    public Account Destination { get; private set; } = destination;
}

public class CheckBalanceOperation(UserAccount source) : AccountOperation(source, source.Balance);
