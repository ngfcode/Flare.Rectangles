using Ardalis.GuardClauses;
using Flare.Rectangles.Application.Common.Interfaces;
using FluentValidation;

namespace Flare.Rectangles.Application.GridLayouts;

public sealed class CreateGridLayoutCommandValidator : AbstractValidator<GridLayoutDto>
{
	public CreateGridLayoutCommandValidator()
	{
		RuleFor(_ => _.Width)
			.Cascade(CascadeMode.Stop)
			.GreaterThanOrEqualTo(Shared.DefaultValues.MinGridSize)
			.WithMessage($"Width must be at least {Shared.DefaultValues.MinGridSize} in width.")
			.LessThanOrEqualTo(Shared.DefaultValues.MaxGridSize)
			.WithMessage($"Width should not exceed {Shared.DefaultValues.MaxGridSize} in width.");

		RuleFor(_ => _.Height)
			.Cascade(CascadeMode.Stop)
			.GreaterThanOrEqualTo(Shared.DefaultValues.MinGridSize)
			.WithMessage($"Height must be at least {Shared.DefaultValues.MinGridSize} in height.")
			.LessThanOrEqualTo(Shared.DefaultValues.MaxGridSize)
			.WithMessage($"Height should not exceed {Shared.DefaultValues.MaxGridSize} in height.");
	}
}
