using FluentValidation;

namespace SaaS.Application.Emails.Send;
internal sealed class SendEmailCommandValidator : AbstractValidator<SendEmailCommand>
{
    public SendEmailCommandValidator()
    {
        this.RuleFor(x => x.RecipientEmail)
            .NotEmpty()
            .EmailAddress();

        this.RuleFor(x => x.Subject)
            .NotEmpty()
            .MaximumLength(200);

        this.RuleFor(x => x.Body)
            .NotEmpty();
    }
}