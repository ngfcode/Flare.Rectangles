namespace Flare.Rectangles.Unit.Test.GridServiceTest;

public sealed class GridServiceCreateGridTest : BaseGridServiceTest
{
	[Theory]
	[InlineData(Shared.DefaultValues.MinGridSize, Shared.DefaultValues.MaxGridSize)]
	[InlineData(Shared.DefaultValues.MinGridSize, Shared.DefaultValues.MinGridSize)]
	[InlineData(Shared.DefaultValues.MaxGridSize, Shared.DefaultValues.MaxGridSize)]
	public void CreateGridWhenWidthHeightValidReturnsOk(
		int width,
		int height)
	{
		// Arrange

		// Act
		var result = _sut.CreateGrid(width, height);

		// Assert
		Assert.NotNull(_sut.GridLayoutDto);
		Assert.True(result.NoErrors);
		Assert.Equal(width, result.Dto.Width);
		Assert.Equal(height, result.Dto.Height);
	}

	[Fact]
	public void CreateGridWhenAlreadyExistReturnsError()
	{
		// Arrange
		_sut.CreateGrid(Shared.DefaultValues.MinGridSize, Shared.DefaultValues.MaxGridSize);

		// Act
		var result = _sut.CreateGrid(Shared.DefaultValues.MinGridSize, Shared.DefaultValues.MaxGridSize);

		// Assert
		Assert.Collection(result.Errors,
			error => Assert.Equal("Grid is already created.", error));
	}

	[Theory]
	[InlineData(Shared.DefaultValues.MinGridSize - 1, Shared.DefaultValues.MinGridSize - 1)]
	[InlineData(Shared.DefaultValues.MaxGridSize + 1, Shared.DefaultValues.MaxGridSize + 1)]
	[InlineData(Shared.DefaultValues.MinGridSize, Shared.DefaultValues.MinGridSize - 1)]
	[InlineData(Shared.DefaultValues.MaxGridSize + 1, Shared.DefaultValues.MaxGridSize)]
	public void CreateGridWhenWidthHeightInvalidReturnsError(
		int width,
		int height)
	{
		// Arrange
		var result = _sut.CreateGrid(width, height);

		// Assert
		Assert.False(result.NoErrors);
	}
}
