using System.Drawing;
using Ardalis.GuardClauses;
using Flare.Rectangles.Application.Common.Interfaces;
using FluentValidation;

namespace Flare.Rectangles.Application.Rectangles;

public sealed class CreateRectangleCommandValidator : AbstractValidator<RectangleDto>
{
	private readonly IGridService _service;

	public CreateRectangleCommandValidator(
		IGridService service)
	{
		_service = Guard.Against.Null(service, nameof(service));

		// Validates the X Position and Width
		When(_ => _.Position.XPosition >= 0, () =>
		{
			When(_ => _.Width > 0, () =>
			{
				RuleFor(_ => _.Width + _.Position.XPosition)
					.Cascade(CascadeMode.Stop)
					.GreaterThan(0)
						.WithMessage("Width must be greater than zero.")
					.Must((width) => DoesNotExceedGridWidth(width))
						.WithMessage("Width should not exceed grid's width.");
			})
			.Otherwise(() =>
			{
				RuleFor(_ => _.Width)
					.GreaterThan(0)
						.WithMessage("Width must be greater than zero.");
			});
		})
		.Otherwise(() =>
		{
			RuleFor(_ => _.Position.XPosition)
			.GreaterThanOrEqualTo(0)
				.WithMessage("X Posiion of the rectangle must be a positive value.");
		});

		// Validates the Y Position and Height
		When(_ => _.Position.YPosition >= 0, () =>
		{
			When(_ => _.Height > 0, () =>
			{
				RuleFor(_ => _.Height + _.Position.YPosition)
					.Cascade(CascadeMode.Stop)
					.GreaterThan(0)
						.WithMessage("Height must be greater than zero.")
					.Must((height) => DoesNotExceedGridHeight(height))
						.WithMessage("Height should not exceed grid's height.");
			})
			.Otherwise(() =>
			{
				RuleFor(_ => _.Height)
					.GreaterThan(0)
						.WithMessage("Height must be greater than zero.");
			});
		})
		.Otherwise(() =>
		{
			RuleFor(_ => _.Position.YPosition)
			.GreaterThanOrEqualTo(0)
				.WithMessage("Y Posiion of the rectangle must be a positive value.");
		});

		// Checks for overlapping rectangle
		RuleFor(dto => dto)
			.Must((dto) => !RectangleIntersect(dto))
				.WithMessage("The rectangle overlaps with anonther rectangle.")
			.When(_ => _.Width > 0 && _.Height > 0 && _.Position.XPosition >= 0 && _.Position.YPosition >= 0);
	}

	private bool DoesNotExceedGridWidth(
		int width)
	{
		return width <= _service.GridLayoutDto?.Width;
	}

	private bool DoesNotExceedGridHeight(
		int height)
	{
		return height <= _service.GridLayoutDto?.Height;
	}

	private bool RectangleIntersect(
		RectangleDto dto)
	{
		var rectIntersect = false;

		var rect = new Rectangle(dto.Position.XPosition, dto.Position.YPosition, dto.Width, dto.Height);
		if (_service.GridLayoutDto is not null)
		{

			// An alternative is to have GridLayoutDto.RectangleDtos is a list of Drawing.Rectangle instead,
			// then use Mapster to map it to another object that will be used as return object to the end-user
			foreach (var data in _service.GridLayoutDto.RectangleDtos)
			{

				if (rect.IntersectsWith(new Rectangle(data.Position.XPosition, data.Position.YPosition, data.Width, data.Height)))
				{

					rectIntersect = true;
					break;

				}
			}
		}

		return rectIntersect;
	}
}
