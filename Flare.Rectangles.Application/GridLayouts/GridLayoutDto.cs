using System.Diagnostics.CodeAnalysis;
using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Application.GridLayouts;

[ExcludeFromCodeCoverage]
public sealed class GridLayoutDto
{
	public required List<RectangleDto> RectangleDtos { get; init; }
	public int Width { get; set; }
	public int Height { get; set; }

	public GridLayoutDto()
	{
		RectangleDtos = new List<RectangleDto>();
	}

	public GridLayoutDto(
		int width,
		int height)
		: this()
	{
		Width = width;
		Height = height;
	}
}
