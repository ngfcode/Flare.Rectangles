using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Unit.Test.GridServiceTest;

public sealed class GridServiceFindRectangleTest : BaseGridServiceTest
{
	public GridServiceFindRectangleTest()
		: base()
	{
		// Creates the grid
		_sut.CreateGrid(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize);
	}

	[Fact]
	public void FindRectangleWhenPositionValidReturnsOk()
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = 10,
			Height = 12,
			Position = new Position
			{
				XPosition = 7,
				YPosition = 5
			}
		};
		_sut.PlaceRectangle(dto);

		// Act
		var result = _sut.FindRectangle(dto.Position.XPosition, dto.Position.YPosition);

		// Assert
		Assert.NotNull(result);
		Assert.IsType<RectangleDto>(result);
	}

	[Fact]
	public void FindRectangleWhenPositionOutsideRectangleReturnsOk()
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = 10,
			Height = 12,
			Position = new Position
			{
				XPosition = 7,
				YPosition = 5
			}
		};
		_sut.PlaceRectangle(dto);

		// Act
		var result = _sut.FindRectangle(dto.Position.XPosition - 1, dto.Position.YPosition - 1);

		// Assert
		Assert.Null(result);
	}

	[Fact]
	public void FindRectangleWhenPositionInsideRectangleReturnsOk()
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = 10,
			Height = 12,
			Position = new Position
			{
				XPosition = 7,
				YPosition = 5
			}
		};
		_sut.PlaceRectangle(dto);

		// Act
		var result = _sut.FindRectangle(dto.Position.XPosition + 1, dto.Position.YPosition + 1);

		// Assert
		Assert.Null(result);
	}
}
