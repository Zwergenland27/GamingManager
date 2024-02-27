using CleanDomainValidation.Domain;
using GamingManager.Domain.DomainErrors;
using System.ComponentModel.DataAnnotations;

namespace GamingManager.Domain.Users.ValueObjects;

public class Email
{
    private Email(string value)
    {
        Value = value;
    }
    public string Value { get; private init; }

    public CanFail<Email> Create(string value)
    {
        if (!new EmailAddressAttribute().IsValid(value))
        {
            return Errors.Users.Email.Invalid(value);
        }

        return new Email(value);
    }
}
