using Flare.Rectangles.Application.Common.Results;
using Flare.Rectangles.Application.GridLayouts;
using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Application.Common.Interfaces;

public interface IGridService
{
	GridLayoutDto GridLayoutDto { get; }

	DtoResult<GridLayoutDto> CreateGrid(
		int width,
		int height);

	DtoResult<RectangleDto> CreateRectangle(
		RectangleDto dto);

	ErrorResult DeleteRectangle(
		int xPos,
		int yPos);

	RectangleDto FindRectangle(
		int xPos,
		int yPos);

	List<RectangleDto> GetRectangles();
}
