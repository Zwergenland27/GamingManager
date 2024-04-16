using CleanDomainValidation.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GamingManager.WebApi.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
	protected OkObjectResult Ok<TResult>(CanFail<TResult> result)
	{
		return Ok(result.Value);
	}

	protected ObjectResult Problem(AbstractCanFail result)
	{
		Dictionary<string, List<Error>> errorsDictionary = [];
		foreach (Error error in result.Errors)
		{
			string errorField = error.Code.Remove(error.Code.LastIndexOf('.'));
			if (!errorsDictionary.ContainsKey(errorField))
			{
				errorsDictionary.Add(errorField, []);
			}
			errorsDictionary[errorField].Add(error);
		}

		ProblemDetails problemDetails = new()
		{
			Status = GetStatusCodeFromResult(result),
			Title = GetTitleFromResult(result),
			Detail = "" //TODO: result.GetSummary()
		};
		problemDetails.Extensions.Add("errors", errorsDictionary);

		return new ObjectResult(problemDetails);
	}

	private static string GetTitleFromResult(AbstractCanFail result)
	{
		if (result.Type == FailureType.ManyDifferent) return "Multiple";
		return result.Errors[0].Type.ToString();
	}

	private static int GetStatusCodeFromResult(AbstractCanFail result)
	{
		if (result.Type == FailureType.ManyDifferent) return StatusCodes.Status400BadRequest;
		return result.Errors[0].Type switch
		{
			ErrorType.Conflict => StatusCodes.Status409Conflict,
			ErrorType.NotFound => StatusCodes.Status404NotFound,
			ErrorType.Validation => StatusCodes.Status400BadRequest,
			ErrorType.Unexpected => StatusCodes.Status500InternalServerError,
			_ => throw new NotImplementedException()
		};
	}
}
