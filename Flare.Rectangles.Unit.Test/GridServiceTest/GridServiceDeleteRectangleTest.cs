using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Unit.Test.GridServiceTest;

public sealed class GridServiceDeleteRectangleTest : BaseGridServiceTest
{
	public GridServiceDeleteRectangleTest()
		: base()
	{
		// Creates the grid
		_sut.CreateGrid(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize);
	}

	[Theory]
	[InlineData(7, 5)] // TopLeft
	[InlineData(16, 5)] // TopRight
	[InlineData(7, 16)] // BottomLeft
	[InlineData(16, 16)] // BottomRight
	[InlineData(11, 10)] // Middle
	public void DeleteRectangleWhenPositionValidReturnsOk(
		int xPos,
		int yPos)
	{
		// Arrange
		_sut.PlaceRectangle(new RectangleDto
		{
			Width = 10,
			Height = 12,
			Position = new Position
			{
				XPosition = 7,
				YPosition = 5
			}
		});

		// Act
		var result = _sut.DeleteRectangle(xPos, yPos);

		// Assert
		Assert.True(result.NoErrors);
	}

	[Theory]
	[InlineData(6, 4)]
	[InlineData(17, 4)]
	[InlineData(6, 17)]
	[InlineData(17, 17)]
	public void DeleteRectangleWhenPositionOutsideRectangleReturnsOk(
		int xPos,
		int yPos)
	{
		// Arrange
		_sut.PlaceRectangle(new RectangleDto
		{
			Width = 10,
			Height = 12,
			Position = new Position
			{
				XPosition = 7,
				YPosition = 5
			}
		});

		// Act
		var result = _sut.DeleteRectangle(xPos, yPos);

		// Assert
		Assert.False(result.NoErrors);
		Assert.Collection(result.Errors,
			error => Assert.Equal("Rectangle not found.", error));
	}
}
