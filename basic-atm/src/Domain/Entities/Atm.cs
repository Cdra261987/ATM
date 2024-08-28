using Domain.Abstractions;

namespace Domain.Entities;


public class Atm : BankingSystem, IUserLogin, IAtmOperation
{
    public Card UsedCard { get; private set; }

    public Atm(List<Account> accounts)
        : base(accounts)
    {
    }

    public void AssociateMbWayNumber(string mobileNumber)
    {
        throw new NotImplementedException();
    }

    public (bool IsSuccess, int Attempts) Login(IUserInteraction userInteraction)
    {
        if (userInteraction.GetType() != typeof(Card))
        {
            throw new Exception("Invalid type of interaction with ATM system! Only Cards are supported");
        }

        var userCard = userInteraction as Card;
        
        var account = _accounts.FirstOrDefault(account => account.AvailableCards
            .Any(card => card.IdCard == userCard.IdCard));

        if (account is null)
        {
            throw new InvalidOperationException("ID Card provided does not exist in current list of accounts!");
        }

        var accountLinkedCard = account.AvailableCards.FirstOrDefault(card => card.IdCard == userCard.IdCard);
        
        if (!accountLinkedCard.Validate(userCard.Pin))
        {
            accountLinkedCard.IncrementFailedAttempts();
            throw new InvalidOperationException($"Provided pin is invalid! You have {accountLinkedCard.LoginRemainingAttempts} attempts remaining");
        }

        LoggedInAccount = account as UserAccount;
        UsedCard = accountLinkedCard;
        return (true, accountLinkedCard.LoginRemainingAttempts);
        
    }
}