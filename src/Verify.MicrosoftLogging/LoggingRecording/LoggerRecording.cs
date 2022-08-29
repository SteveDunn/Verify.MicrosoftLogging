﻿using Microsoft.Extensions.Logging;

namespace VerifyTests;

public static class LoggerRecording
{
    static AsyncLocal<LoggerProvider?> local = new();

    public static LoggerProvider Start(LogLevel logLevel = LogLevel.Information) =>
        local.Value = new(logLevel);

    public static bool TryFinishRecording(out IEnumerable<object>? entries)
    {
        var provider = local.Value;

        if (provider is null)
        {
            local.Value = null;
            entries = null;
            return false;
        }

        entries = provider.entries.ToArray();
        local.Value = null;
        return true;
    }
}