using System.Collections.Generic;
using KaliGasService.Core.Application.CQRS;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Warehousing.API.Application.Errors
{
    internal static class ApiErrorResult
    {
        public static IActionResult Create(int statusCode, string errorMessage)
        {
            return StatusCode(statusCode, new ApiError(statusCode, ReasonPhrases.GetReasonPhrase(statusCode), errorMessage));
        }

        public static IActionResult Create(int statusCode, IEnumerable<Error> errors)
        {
            return StatusCode(statusCode, new ApiError(statusCode, ReasonPhrases.GetReasonPhrase(statusCode), ErrorFormatter.Format(errors)));
        }

        private static ObjectResult StatusCode(int statusCode, object value)
        {
            return new ObjectResult(value)
            {
                StatusCode = statusCode
            };
        }
    }
}
