using Ardalis.GuardClauses;
using Flare.Rectangles.Application.Common.Interfaces;
using Flare.Rectangles.Application.Rectangles;
using Microsoft.AspNetCore.Mvc;

namespace Flare.Rectangles.WebApi.Controllers;
[Route("api/[controller]")]
[ApiController]
public class GridServiceController : ControllerBase
{
	private readonly IGridService _service;

	public GridServiceController(
		IGridService service)
	{
		_service = Guard.Against.Null(service, nameof(service));
	}

	[HttpPost("/create-grid")]
	public async Task<IActionResult> CreateGrid(
		int width,
		int height)
	{
		var result = _service.CreateGrid(width, height);
		return result.NoErrors ? Ok(result) : BadRequest(result);
	}

	[HttpPost("/place-rectangle")]
	public async Task<IActionResult> CreateRectangle(
		RectangleDto dto)
	{
		var result = _service.CreateRectangle(dto);
		return result.NoErrors ? Ok(result) : BadRequest(result);
	}

	[HttpGet("/rectangle")]
	public async Task<IActionResult> GetRectangle(
		int xPos,
		int yPos)
	{
		var dto = _service.FindRectangle(xPos, yPos);
		return dto is not null ? Ok(dto) : NotFound();
	}

	[HttpGet("/rectangles")]
	public async Task<IActionResult> GetRectangles()
	{
		return Ok(_service.GetRectangles());
	}

	[HttpDelete("/delete-rectangle")]
	public async Task<IActionResult> DeleteRectangle(
		int xPos,
		int yPos)
	{
		var result = _service.DeleteRectangle(xPos, yPos);
		return result.NoErrors ? Ok(result) : BadRequest(result);
	}
}
