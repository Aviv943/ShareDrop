using Microsoft.AspNetCore.Mvc;
using ShareDropApi.Application.Requests;
using ShareDropApi.Application.Services.Interfaces;

namespace ShareDropApi.Controllers;

[Route("api/copy")]
[ApiController]
public class CopyController : ControllerBase
{
    private readonly ICopyService _copyService;

    public CopyController(ICopyService copyService)
    {
        _copyService = copyService;
    }

    [HttpPost]
    public IActionResult CreateAsync([FromBody] CreateCopyRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Text))
        {
            return BadRequest("Text is null or empty");
        }

        _copyService.CreateAsync(request.Text);
        return Ok();
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var text = await _copyService.GetAsync();

        if (string.IsNullOrWhiteSpace(text))
        {
            return NoContent();
        }

        return Ok(text);
    }
}