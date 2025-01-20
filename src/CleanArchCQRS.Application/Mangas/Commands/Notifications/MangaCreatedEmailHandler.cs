using MediatR;

using Microsoft.Extensions.Logging;

namespace CleanArchCQRS.Application.Mangas.Commands.Notifications;

public class MangaCreatedEmailHandler : INotificationHandler<MangaCreatedNotification>
{
    private readonly ILogger<MangaCreatedEmailHandler> _logger;

    public MangaCreatedEmailHandler(ILogger<MangaCreatedEmailHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(MangaCreatedNotification notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"Confirmation email sent for : {notification.Manga.Title}");

        // TODO: send a confirmation email

        return Task.CompletedTask;
    }
}
