using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Domain.Users.ValueObjects;

public sealed record Email
{
    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; private init; }

    public static CanFail<Email> Create(string value)
    {
        if (!new EmailAddressAttribute().IsValid(value))
        {
            return Errors.Users.Email.Invalid;
        }

        return new Email(value);
    }
}
