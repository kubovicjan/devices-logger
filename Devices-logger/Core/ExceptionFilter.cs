// Copyright (c) Jan Kubovic All rights reserved.
// ExceptionFilter.cs

namespace DevicesLogger.Core;

using System.Linq;
using System.Threading.Tasks;
using DevicesLogger.Domain.Errors;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ExceptionFilter : IAsyncActionFilter
{
    private readonly ILogger<ExceptionFilter> _logger;

    public ExceptionFilter(ILogger<ExceptionFilter> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(next);

        _logger.LogInformation($"Started processing request {context.HttpContext.Request.Path}...");

        var executedContext = await next().ConfigureAwait(false);
        ErrorResponse response = null!;
        var action = (int statusCode) =>
        {
            executedContext.Result = new ObjectResult(response)
            {
                StatusCode = statusCode
            };
            executedContext.ExceptionHandled = true;
        };

        switch (executedContext.Exception)
        {
            case ValidationException vex:
                _logger.LogError(vex, vex.Message);
                response = new ErrorResponse()
                {
                    ErrorCode = (int)ErrorCodes.InvalidInputData,
                    ErrorMessage = string.Join(Environment.NewLine, vex.Errors.ToList().Select(e => e.ErrorMessage))
                };
                action.Invoke(StatusCodes.Status400BadRequest);
                break;
            case InvalidOperationException ioe:
                _logger.LogError(ioe, ioe.Message);
                response = new ErrorResponse()
                {
                    ErrorCode = (int)ErrorCodes.InvalidInputData,
                    ErrorMessage = ioe.Message
                };
                action.Invoke(StatusCodes.Status400BadRequest);
                break;
            case Exception ex:
                _logger.LogError(ex, ex.Message);
                response = new ErrorResponse()
                {
                    ErrorCode = (int)ErrorCodes.Unknown,
                    ErrorMessage = ex.Message
                };
                action.Invoke(StatusCodes.Status500InternalServerError);
                break;
        }

        _logger.LogInformation($"Finished processing request {context.HttpContext.Request.Path}...");
    }
}
