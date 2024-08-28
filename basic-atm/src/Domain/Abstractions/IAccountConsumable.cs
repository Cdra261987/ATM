using Domain.Entities;

namespace Domain.Abstractions;

public interface IAccountConsumable
{
    bool FreezeCard(UserAccount account, string idCard);
}