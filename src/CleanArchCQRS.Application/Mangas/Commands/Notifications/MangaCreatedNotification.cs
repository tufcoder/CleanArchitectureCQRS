using CleanArchCQRS.Domain.Models;

using MediatR;

namespace CleanArchCQRS.Application.Mangas.Commands.Notifications;

public class MangaCreatedNotification : INotification
{
    public Manga Manga { get; set; }

    public MangaCreatedNotification(Manga manga)
    {
        Manga = manga;
    }
}
