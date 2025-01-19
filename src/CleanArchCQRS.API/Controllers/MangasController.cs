using CleanArchCQRS.Application.Mangas.Commands;
using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchCQRS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MangasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;

    //public MangasController(IUnitOfWork unitOfWork)
    //{
    //    _unitOfWork = unitOfWork;
    //}

    //public MangasController(IMediator mediator)
    //{
    //    _mediator = mediator;
    //}

    public MangasController(IUnitOfWork unitOfWork, IMediator mediator)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var mangas = await _unitOfWork.MangaRepository.GetAll();
        return mangas is null ? NotFound() : Ok(mangas);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var manga = await _unitOfWork.MangaRepository.GetById(id);

            return Ok(manga);
        }
        catch (Exception ex)
        {
            return ex is InvalidOperationException ? NotFound() : BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateManga(CreateMangaCommand command)
    {
        try
        {
            var manga = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id = manga.Id }, manga);
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
