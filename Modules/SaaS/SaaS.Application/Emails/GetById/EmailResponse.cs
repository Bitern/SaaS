namespace SaaS.Application.Emails.GetById;
public sealed record EmailResponse
{
    public Guid Id { get; set; }
    public string Subject { get; set; } = string.Empty;
    public string Body { get; set; } = string.Empty;
    public string RecipientEmail { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}