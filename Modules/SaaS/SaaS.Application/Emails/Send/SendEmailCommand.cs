using Application.Abstractions.Messaging;

namespace SaaS.Application.Emails.Send;
public sealed record SendEmailCommand(string Subject, string Body, string RecipientEmail) : ICommand;
