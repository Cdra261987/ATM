using Domain.Entities;

namespace Domain.Abstractions;

public interface IUserOperation
{
    void Deposit(decimal amount);
    void Transfer(UserAccount destination, decimal amount);
    void Withdraw(decimal amount);
    decimal CheckBalance();
}