using Application.Abstractions.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SaaS.Domain.Emails;
using SaaS.SharedKernel;
using System.Net;
using System.Net.Mail;

namespace SaaS.Application.Emails.EventHandlers;

internal sealed class EmailSentDomainEventHandler(
    IApplicationDbContext dbContext,
    IConfiguration configuration,
    ILogger<EmailSentDomainEventHandler> logger) : IDomainEventHandler<EmailSentDomainEvent>
{
    public async Task Handle(EmailSentDomainEvent notification, CancellationToken cancellationToken)
    {
        Email? emailEntity = await dbContext.Emails
            .FirstOrDefaultAsync(e => e.Id == notification.EmailId, cancellationToken);

        if (emailEntity is null)
        {
            logger.LogWarning("Email with ID {EmailId} not found for sending.", notification.EmailId);
            return;
        }

        if (emailEntity.Status != EmailStatus.Pending)
        {
            logger.LogInformation(
                "Email with ID {EmailId} is not in Pending status (Status: {Status}). Skipping send.",
                emailEntity.Id,
                emailEntity.Status);
            return;
        }

        Result sendOutcome = await this.TrySendEmailAsync(emailEntity, cancellationToken);

        if (sendOutcome.IsSuccess)
        {
            emailEntity.MarkAsSent();
            logger.LogInformation(
                "Email with ID {EmailId} marked as sent to {RecipientEmail}.",
                emailEntity.Id,
                emailEntity.RecipientEmail);
        }
        else
        {
            emailEntity.MarkAsFailed(sendOutcome.Error?.Description ?? "Unknown SMTP error during send.");
            logger.LogError(
                "Failed to send email with ID {EmailId} to {RecipientEmail}. Error: {Error}",
                emailEntity.Id,
                emailEntity.RecipientEmail,
                sendOutcome.Error);
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    private async Task<Result> TrySendEmailAsync(Email email, CancellationToken cancellationToken)
    {
        try
        {
            var smtpHost = configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"] ?? "587");
            var smtpUsername = configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = configuration["EmailSettings:SmtpPassword"];
            var smtpEnableSsl = bool.Parse(configuration["EmailSettings:EnableSsl"] ?? "true");
            var senderEmail = configuration["EmailSettings:SenderEmail"];
            var senderName = configuration["EmailSettings:SenderName"];

            if (string.IsNullOrEmpty(smtpHost) || string.IsNullOrEmpty(senderEmail))
            {
                logger.LogError("SMTP Host or Sender Email not configured. Cannot send email ID {EmailId}.", email.Id);
                return Result.Failure(Error.Conflict(
                    "Email.SmtpConfigurationMissing",
                    "SMTP Host or Sender Email not configured."));
            }

            using var smtpClient = new SmtpClient(smtpHost, smtpPort);
            if (!string.IsNullOrEmpty(smtpUsername) && !string.IsNullOrEmpty(smtpPassword))
            {
                smtpClient.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
            }
            smtpClient.EnableSsl = smtpEnableSsl;

            using var mailMessage = new MailMessage
            {
                From = new MailAddress(senderEmail, senderName),
                Subject = email.Subject,
                Body = email.Body,
                IsBodyHtml = false,
            };
            mailMessage.To.Add(email.RecipientEmail);

            await smtpClient.SendMailAsync(mailMessage, cancellationToken);

            logger.LogInformation(
                "SMTP call successful for Email ID {EmailId} to {RecipientEmail}.",
                email.Id,
                email.RecipientEmail);

            return Result.Success();
        }
        catch (SmtpException smtpEx)
        {
            logger.LogError(
                smtpEx,
                "SmtpException while sending Email ID {EmailId} to {RecipientEmail}. Status Code: {StatusCode}",
                email.Id,
                email.RecipientEmail,
                smtpEx.StatusCode);
            return Result.Failure(Error.Failure(
                "Email.SmtpException",
                $"SMTP error: {smtpEx.Message} (Code: {smtpEx.StatusCode})"));
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Generic exception while sending Email ID {EmailId} to {RecipientEmail}.",
                email.Id,
                email.RecipientEmail);
            return Result.Failure(Error.Failure(
                 "Email.SendGenericError",
                 $"Generic error during email send: {ex.Message}"));
        }
    }
}