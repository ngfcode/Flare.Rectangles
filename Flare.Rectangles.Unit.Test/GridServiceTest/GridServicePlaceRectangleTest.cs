using Flare.Rectangles.Application.Rectangles;

namespace Flare.Rectangles.Unit.Test.GridServiceTest;

public sealed class GridServicePlaceRectangleTest : BaseGridServiceTest
{
	public GridServicePlaceRectangleTest()
		: base()
	{
		// Creates the grid
		_sut.CreateGrid(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize);
	}

	[Theory]
	[InlineData(1, 1, 0, 0)]
	[InlineData(Shared.DefaultValues.MaxGridSize - 1, Shared.DefaultValues.MaxGridSize - 1, 0, 0)]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize, 0, 0)]
	public void PlaceRectangleWhenRectangleDtoValidReturnsOk(
		int width,
		int height,
		int xPos,
		int yPos)
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.True(result.NoErrors);
	}

	[Theory]
	[InlineData(10, 2, 7, 3)]
	[InlineData(10, 2, 7, 17)]
	[InlineData(2, 12, 5, 5)]
	[InlineData(2, 12, 17, 5)]
	public void PlaceRectangleWhenAdjacentReturnsOk(
		int width,
		int height,
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
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.True(result.NoErrors);
	}

	[Theory]
	[InlineData(5, 6, 4, 2)]
	[InlineData(5, 6, 15, 2)]
	[InlineData(5, 6, 4, 15)]
	[InlineData(5, 6, 15, 15)]
	public void PlaceRectangleWhenRectangleCornerOverlapReturnsError(
		int width,
		int height,
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
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
		Assert.Collection(result.Errors,
			error => Assert.Equal("The rectangle overlaps with anonther rectangle.", error));
	}

	[Theory]
	[InlineData(8, 10, 8, 6)]
	[InlineData(12, 14, 6, 4)]
	public void PlaceRectangleWhenRectangleOnTopOfAnotherReturnsError(
		int width,
		int height,
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
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
		Assert.Collection(result.Errors,
			error => Assert.Equal("The rectangle overlaps with anonther rectangle.", error));
	}

	[Fact]
	public void PlaceRectangleWhenRectangleSamePropertiesReturnsError()
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
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
		Assert.Collection(result.Errors,
			error => Assert.Equal("The rectangle overlaps with anonther rectangle.", error));
	}

	[Theory]
	[InlineData(0, 1, 0, 0)]
	[InlineData(1, 0, 0, 0)]
	[InlineData(Shared.DefaultValues.MaxGridSize + 1, 1, 0, 0)]
	[InlineData(1, Shared.DefaultValues.MaxGridSize + 1, 0, 0)]
	public void PlaceRectangleWhenWidthHeightInvalidReturnsError(
		int width,
		int height,
		int xPos,
		int yPos)
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
	}

	[Theory]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize, 1, 0)]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize, 0, 1)]
	public void PlaceRectangleWhenRectangleGoesBeyondGridReturnsError(
		int width,
		int height,
		int xPos,
		int yPos)
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
	}
	[Theory]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize, -1, 0)]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize, 0, -1)]
	public void PlaceRectangleWhenPositionInvalidReturnsError(
		int width,
		int height,
		int xPos,
		int yPos)
	{
		// Arrange
		var dto = new RectangleDto
		{
			Width = width,
			Height = height,
			Position = new Position
			{
				XPosition = xPos,
				YPosition = yPos
			}
		};

		// Act
		var result = _sut.PlaceRectangle(dto);

		// Assert
		Assert.False(result.NoErrors);
	}
}
