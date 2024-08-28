using ATM.Domain.Exceptions;
using Domain.Abstractions;

namespace Domain.Entities;

public class OnlineBankingSystem : BankingSystem, IUserLogin, IOnlineBankingSystem
{
    public OnlineBankingSystem(List<Account> accounts)
        :base(accounts)
    {
    }
    
    public (bool IsSuccess, int Attempts) Login(IUserInteraction userInteraction)
    {
        if (userInteraction.GetType() != typeof(UserAccount))
        {
            throw new Exception("Invalid type of interaction with Online banking system! Only user accounts are supported");
        }

        var userAccount = userInteraction as UserAccount;
        var accountLoggingIn = _accounts.FirstOrDefault(account => account.Username == userAccount.Username) as UserAccount;

        if (accountLoggingIn.Locked)
            throw new InvalidAccountException($"Account is currently locked since it has reached maximum number of invalid attempts!" +
                                              $" Please reach out to account administrator");
        
        if (!accountLoggingIn.Validate(userAccount.Password))
        {
            var attempts = accountLoggingIn.IncrementFailedAttempts();
            if (attempts == 0)
            {
                accountLoggingIn.Lock();
                throw new InvalidAccountException($"Account with username '{userAccount.Username}' is locked due to suspicious activity. Please contact administrator");
            }
            throw new InvalidAccountException($"Invalid login attempt for user with username '{userAccount.Username}'");
        }

        LoggedInAccount = accountLoggingIn;
        return (true, accountLoggingIn.LoginAttempts);
    }
}