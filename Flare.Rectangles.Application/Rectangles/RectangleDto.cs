using System.Diagnostics.CodeAnalysis;

namespace Flare.Rectangles.Application.Rectangles;

[ExcludeFromCodeCoverage]
public sealed class RectangleDto
{
	public Guid Id { get; init; } = new Guid();
	public required int Width { get; init; }
	public required int Height { get; init; }
	public required Position Position { get; set; }
}

public record Position
{
	public int XPosition { get; init; }
	public int YPosition { get; init; }
}
