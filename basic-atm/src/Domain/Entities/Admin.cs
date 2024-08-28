using Domain.Abstractions;

namespace Domain.Entities;

public class Admin : Account, IUserManagement, IAccountConsumable
{
    public Admin(string username, string password) 
        : base(username, password)
    {
    }

    public bool UnlockAccount(UserAccount account) => account.Unlock(true);
    
    public bool FreezeCard(UserAccount account, string idCard)
    {
        var userCard = account.AvailableCards.FirstOrDefault(card => card.IdCard == idCard);

        if (userCard != null && userCard.Locked)
            throw new InvalidOperationException($"Card associated with account is already locked!");
        
        userCard.Lock();
        return true;
    }
}