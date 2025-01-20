using CleanArchCQRS.Application.Mangas.Commands;
using CleanArchCQRS.Application.Mangas.Queries;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchCQRS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MangasController : ControllerBase
{
    private readonly IMediator _mediator;

    public MangasController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetMangas()
    {
        var query = new GetMangasQuery();
        var mangas = await _mediator.Send(query);

        return mangas is null ? NoContent() : Ok(mangas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMangasById(int id)
    {
        var query = new GetMangasByIdQuery { Id = id };
        var manga = await _mediator.Send(query);

        return manga is null ? NotFound() : Ok(manga);
    }

    [HttpPost]
    public async Task<IActionResult> CreateManga(CreateMangaCommand command)
    {
        try
        {
            var manga = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetMangasById), new { id = manga.Id }, manga);
        }
        catch (Exception)
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateManga(int id, UpdateMangaCommand command)
    {
        try
        {
            command.Id = id;

            var manga = await _mediator.Send(command);

            return Ok(manga);

        }
        catch (Exception ex)
        {
            return ex is InvalidOperationException ? NotFound() : BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteManga(int id)
    {
        try
        {
            var command = new DeleteMangaCommand { Id = id };

            var manga = await _mediator.Send(command);

            return Ok(manga);
        }
        catch (Exception ex)
        {
            return ex is InvalidOperationException ? NotFound() : BadRequest();
        }
    }
}
