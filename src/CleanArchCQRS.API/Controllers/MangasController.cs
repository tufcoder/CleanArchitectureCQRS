using CleanArchCQRS.Domain.Abstractions;
using CleanArchCQRS.Domain.Models;

using Microsoft.AspNetCore.Mvc;

namespace CleanArchCQRS.API.Controllers;

[Route("[controller]")]
[ApiController]
public class MangasController : ControllerBase
{
    private readonly IUnitOfWork _unitOfWork;

    public MangasController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
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
    public async Task<IActionResult> Add(Manga newManga)
    {
        try
        {
            if (newManga is null)
            {
                return BadRequest("Invalid manga data.");
            }

            var manga = await _unitOfWork.MangaRepository.Add(newManga);
            await _unitOfWork.CommitAsync();
            return CreatedAtAction(nameof(GetById), new { id = manga.Id }, manga);
        }
        catch (Exception ex)
        {
            return ex is ArgumentNullException ? NotFound() : BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Manga updatedManga)
    {
        try
        {
            var manga = await _unitOfWork.MangaRepository.GetById(id);
            manga.Update(
                updatedManga.Title!,
                updatedManga.Price,
                updatedManga.Genres,
                updatedManga.ReleaseDate,
                updatedManga.Publisher!,
                updatedManga.IsActive
                );
            manga.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.MangaRepository.Update(manga);
            await _unitOfWork.CommitAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return ex is InvalidOperationException ? NotFound() : BadRequest();
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var deletedManga = await _unitOfWork.MangaRepository.DeleteById(id);

            await _unitOfWork.CommitAsync();
            return Ok(deletedManga);
        }
        catch (Exception ex)
        {
            return ex is ArgumentNullException ? NotFound() : BadRequest();
        }
    }
}
