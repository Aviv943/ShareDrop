using Microsoft.AspNetCore.Mvc;
using ShareDropApi.Application.Services.Interfaces;

namespace ShareDropApi.Controllers;

[Route("api/drop")]
[ApiController]
public class DropController : ControllerBase
{
    private readonly IDropService _dropService;

    public DropController(IDropService dropService)
    {
        _dropService = dropService;
    }

    [HttpPost]
    public async Task<IActionResult> UploadAsync(IFormFile? file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("Invalid file");
        }

        Stream stream = file.OpenReadStream();
        await _dropService.UploadAsync(stream, file.FileName);

        return Ok("Image uploaded successfully");
    }
}