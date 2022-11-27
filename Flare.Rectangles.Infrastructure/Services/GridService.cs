using Flare.Rectangles.Application.Common.Interfaces;
using Flare.Rectangles.Application.Common.Results;
using Flare.Rectangles.Application.GridLayouts;
using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Infrastructure.Services;

public sealed class GridService : IGridService
{
	public GridLayoutDto? GridLayoutDto => _data;

	private static GridLayoutDto? _data;

	public DtoResult<GridLayoutDto> CreateGrid(
		int width,
		int height)
	{
		if (GridLayoutDto is not null)
		{
			return new DtoResult<GridLayoutDto>("Grid is already created.");
		}

		var dto = new GridLayoutDto
		{
			RectangleDtos = new List<RectangleDto>(),
			Width = width,
			Height = height
		};

		var valOutput = (new CreateGridLayoutCommandValidator()).Validate(dto);
		if (valOutput.IsValid)
		{

			_data = dto;
			return new DtoResult<GridLayoutDto>(dto);

		}

		var errors = valOutput.Errors
			.Select(_ => _.ErrorMessage)
			.ToList();
		return new DtoResult<GridLayoutDto>(errors);
	}

	public DtoResult<RectangleDto> PlaceRectangle(
		RectangleDto dto)
	{
		if (GridLayoutDto is null)
		{
			return new DtoResult<RectangleDto>("Grid has not yet been created.");
		}

		var valOutput = (new CreateRectangleCommandValidator(this)).Validate(dto);
		if (valOutput.IsValid)
		{

			GridLayoutDto?.RectangleDtos.Add(dto);
			return new DtoResult<RectangleDto>(dto);

		}

		var errors = valOutput.Errors
			.Select(_ => _.ErrorMessage)
			.ToList();
		return new DtoResult<RectangleDto>(errors);
	}

	public RectangleDto FindRectangle(
		int xPos,
		int yPos)
	{
		RectangleDto dto = null;

		if (GridLayoutDto is not null)
		{
			dto = GridLayoutDto?.RectangleDtos
				.Where(_ => _.Position.XPosition == xPos && _.Position.YPosition == yPos)
				.FirstOrDefault();
		}

		return dto;
	}

	public List<RectangleDto> GetRectangles()
	{
		return GridLayoutDto?.RectangleDtos;
	}

	public ErrorResult DeleteRectangle(
		int xPos,
		int yPos)
	{
		if (GridLayoutDto is null)
		{
			return new ErrorResult();
		}

		var valOutput = (new DeleteRectangleCommandValidator()).Validate(
			  new Position { XPosition = xPos, YPosition = yPos });
		if (valOutput.IsValid)
		{

			var dto = GridLayoutDto?.RectangleDtos
				.Where(_ => xPos >= _.Position.XPosition && xPos < (_.Position.XPosition + _.Width)
					&& yPos >= _.Position.YPosition && yPos < (_.Position.YPosition + _.Height))
				.FirstOrDefault();
			if (dto is not null)
			{
				GridLayoutDto?.RectangleDtos.Remove(dto);
				return new ErrorResult();
			}

			return new ErrorResult("Rectangle not found.");

		}

		var errors = valOutput.Errors
			.Select(_ => _.ErrorMessage)
			.ToList();
		return new ErrorResult(errors);
	}

	// This is a utility method for testing purposes only
	public void ClearData()
	{
		_data = null;
	}
}
