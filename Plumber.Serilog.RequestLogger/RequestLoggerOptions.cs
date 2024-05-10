﻿using Serilog;
using Serilog.Events;

namespace Plumber.Serilog;

public sealed class RequestLoggerOptions<TRequest, TResponse>
    where TRequest : class
{
    private const string DefaultCompletedMessage =
        "Request {RequestId} completed in {Elapsed:0.0000} ms";

    private static IEnumerable<LogEventProperty> DefaultGetMessageTemplateProperties(RequestContext<TRequest, TResponse> context) =>
    [
        new LogEventProperty(nameof(RequestContext<TRequest, TResponse>.Id), new ScalarValue(context.Id)),
        new LogEventProperty(nameof(RequestContext<TRequest, TResponse>.Elapsed), new ScalarValue(context.Elapsed.TotalMilliseconds))
    ];

    public Action<IDiagnosticContext, RequestContext<TRequest, TResponse>>? EnrichDiagnosticContext { get; set; }
    public ILogger? Logger { get; set; }
    public LogEventLevel LogLevel { get; set; } = LogEventLevel.Information;

    public string MessageTemplate { get; set; } = DefaultCompletedMessage;
    public Func<RequestContext<TRequest, TResponse>, IEnumerable<LogEventProperty>> GetMessageTemplateProperties { get; set; } = DefaultGetMessageTemplateProperties;

    public bool ThrowOnException { get; set; } = true;
}
