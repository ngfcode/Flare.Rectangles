using System.Diagnostics.CodeAnalysis;

namespace Flare.Rectangles.Application.Common.Results;

[ExcludeFromCodeCoverage]
public class ErrorResult
{
	public List<string> Errors { get; private set; }

	public bool NoErrors => Errors.Count == 0;

	public ErrorResult()
	{
		Errors = new List<string>();
	}

	public ErrorResult(
		string error)
		: this()
	{
		Errors.Add(error);
	}

	public ErrorResult(
		List<string> errors)
		: this()
	{
		Errors = errors;
	}

	public void AddErrorMessage(
		string error)
	{
		Errors.Add(error);
	}

	public void AddErrorMessages(
		List<string> errors,
		bool reset = false)
	{
		if (reset)
		{
			Errors.Clear();
		}

		Errors.AddRange(errors);
	}
}
