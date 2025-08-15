﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace MonthlyRevenue.Filters;
public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;
    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) => _logger = logger;

    public void OnException(ExceptionContext context)
    {
        _logger.LogError(context.Exception, "Unhandled exception");
        context.Result = new ObjectResult(new { error = "Unexpected error." })
        { StatusCode = 500 };
        context.ExceptionHandled = true;
    }
}