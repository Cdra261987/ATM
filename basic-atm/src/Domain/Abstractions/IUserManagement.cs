using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserManagement
{
    bool UnlockAccount(UserAccount account);
}