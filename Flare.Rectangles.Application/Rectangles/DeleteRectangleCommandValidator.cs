﻿using FluentValidation;

namespace Flare.Rectangles.Application.Rectangles;

public sealed class DeleteRectangleCommandValidator : AbstractValidator<Position>
{
	public DeleteRectangleCommandValidator()
	{
		RuleFor(_ => _.XPosition)
			.GreaterThanOrEqualTo(0)
				.WithMessage("X Position must be a positive value.");

		RuleFor(_ => _.YPosition)
			.GreaterThanOrEqualTo(0)
				.WithMessage("Y Position must be a positive value.");
	}
}
