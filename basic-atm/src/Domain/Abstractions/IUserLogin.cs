namespace Domain.Abstractions;

public interface IUserLogin
{
    (bool IsSuccess, int Attempts) Login(IUserInteraction userInteraction);
}