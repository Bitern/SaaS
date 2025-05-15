using SaaS.SharedKernel;

namespace SaaS.Domain.Emails;

public sealed class Email(
    Guid id,
    string recipientEmail,
    string subject,
    string body) : Entity
{
    public Guid Id { get; private set; } = id;
    public string RecipientEmail { get; private set; } = recipientEmail;
    public string? EmailSender { get; set; }
    public string Subject { get; private set; } = subject;
    public string Body { get; private set; } = body;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime? SentAt { get; private set; }
    public EmailStatus Status { get; private set; } = EmailStatus.Pending;
    public string? FailureReason { get; private set; }

    public void MarkAsSent()
    {
        if (Status == EmailStatus.Pending)
        {
            Status = EmailStatus.Sent;
            SentAt = DateTime.UtcNow;
        }
    }

    public void MarkAsFailed(string reason)
    {
        if (Status == EmailStatus.Pending)
        {
            Status = EmailStatus.Failed;
            SentAt = DateTime.UtcNow;
            FailureReason = reason;
        }
    }
}
