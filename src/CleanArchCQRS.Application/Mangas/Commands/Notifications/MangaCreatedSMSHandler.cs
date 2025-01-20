using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchCQRS.Application.Mangas.Commands.Notifications;

public class MangaCreatedSMSHandler : INotificationHandler<MangaCreatedNotification>
{
    private readonly ILogger<MangaCreatedSMSHandler> _logger;

    public MangaCreatedSMSHandler(ILogger<MangaCreatedSMSHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MangaCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation sms sent for : {notification.Manga.Title}");

        // TODO: send sms

        return Task.CompletedTask;
    }
}
