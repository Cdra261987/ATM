using ATM.Domain.Exceptions;
using Domain.Abstractions;

namespace Domain.Entities;

public class Card: IUserInteraction
{
    public string IdCard { get;  set; }
    public string Pin { get; set; }
    public int LoginRemainingAttempts { get; private set; } = 3;
    public bool Locked { get; private set; } = false;

    public Card(string idCard, string pin)
    {
        IdCard = string.IsNullOrWhiteSpace(idCard) 
            ? throw new InvalidAccountException("ID Card cannot be null or empty when creating an account")
            : idCard;
        Pin = string.IsNullOrWhiteSpace(pin) 
            ? throw new InvalidAccountException("PIN cannot be null or empty when creating an account")
            : pin;
    }

    
    public bool Validate(string secret) => Pin == secret;

    public void Lock()
    {
        Locked = true;
    }

    public void Unlock() => Locked = false;

    public int IncrementFailedAttempts() => LoginRemainingAttempts--;
    public int ResetFailedAttempts() => LoginRemainingAttempts = 3;
    
}