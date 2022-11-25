using System.Diagnostics.CodeAnalysis;

namespace Flare.Rectangles.Application.Common.Results;

[ExcludeFromCodeCoverage]
public sealed class DtoResult<T> : ErrorResult
	where T : class
{
	public T Dto { get; set; }

	public DtoResult(
		T dto)
	{
		Dto = dto;
	}

	public DtoResult(
		string error)
	{
		AddErrorMessage(error);
	}

	public DtoResult(
		List<string> errors)
	{
		AddErrorMessages(errors);
	}
}
