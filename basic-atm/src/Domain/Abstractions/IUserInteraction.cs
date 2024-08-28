namespace Domain.Abstractions;

public interface IUserInteraction
{
    bool Validate(string secret);
    int IncrementFailedAttempts();
    int ResetFailedAttempts();
    void Unlock();
    void Lock();
}