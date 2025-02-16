using System.Diagnostics;

using Serilog;

namespace Moon;

public static class Log
{
    private static readonly ILogger _coreLogger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
        .CreateLogger()
        .ForContext("SourceContext", "MOON");

    private static readonly ILogger _clientLogger = new LoggerConfiguration()
        .MinimumLevel.Verbose()
        .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {SourceContext} {Message:lj}{NewLine}{Exception}")
        .CreateLogger()
        .ForContext("SourceContext", "APP");

    #region Core Logger

    [Conditional("MOON_LOG")]
    public static void CoreVerbose(string messageTemplate) => _coreLogger.Verbose(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T>(string messageTemplate, T propertyValue) => _coreLogger.Verbose<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Verbose<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Verbose<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Verbose(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose(Exception? exception, string messageTemplate) => _coreLogger.Verbose(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Verbose<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Verbose<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Verbose<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreVerbose(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Verbose(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreDebug(string messageTemplate) => _coreLogger.Debug(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T>(string messageTemplate, T propertyValue) => _coreLogger.Debug<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Debug<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Debug<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreDebug(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Debug(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreDebug(Exception? exception, string messageTemplate) => _coreLogger.Debug(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Debug<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Debug<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreDebug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Debug<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreDebug(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Debug(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreInformation(string messageTemplate) => _coreLogger.Information(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T>(string messageTemplate, T propertyValue) => _coreLogger.Information<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Information<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Information<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreInformation(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Information(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreInformation(Exception? exception, string messageTemplate) => _coreLogger.Information(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Information<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Information<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreInformation<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Information<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreInformation(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Information(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreWarning(string messageTemplate) => _coreLogger.Warning(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T>(string messageTemplate, T propertyValue) => _coreLogger.Warning<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Warning<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Warning<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreWarning(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Warning(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreWarning(Exception? exception, string messageTemplate) => _coreLogger.Warning(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Warning<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Warning<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreWarning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Warning<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreWarning(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Warning(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreError(string messageTemplate) => _coreLogger.Error(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreError<T>(string messageTemplate, T propertyValue) => _coreLogger.Error<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreError<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Error<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreError<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Error<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreError(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Error(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreError(Exception? exception, string messageTemplate) => _coreLogger.Error(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreError<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Error<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreError<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Error<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreError<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Error<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreError(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Error(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreFatal(string messageTemplate) => _coreLogger.Fatal(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T>(string messageTemplate, T propertyValue) => _coreLogger.Fatal<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Fatal<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Fatal<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreFatal(string messageTemplate, params object?[]? propertyValues) => _coreLogger.Fatal(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void CoreFatal(Exception? exception, string messageTemplate) => _coreLogger.Fatal(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T>(Exception? exception, string messageTemplate, T propertyValue) => _coreLogger.Fatal<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _coreLogger.Fatal<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void CoreFatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _coreLogger.Fatal<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void CoreFatal(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _coreLogger.Fatal(exception, messageTemplate, propertyValues);

    #endregion

    #region Client Logger

    [Conditional("MOON_LOG")]
    public static void Verbose(string messageTemplate) => _clientLogger.Verbose(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Verbose<T>(string messageTemplate, T propertyValue) => _clientLogger.Verbose<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Verbose<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Verbose<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Verbose<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Verbose<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Verbose(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Verbose(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Verbose(Exception? exception, string messageTemplate) => _clientLogger.Verbose(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Verbose<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Verbose<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Verbose<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Verbose<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Verbose<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Verbose<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Verbose(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Verbose(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Debug(string messageTemplate) => _clientLogger.Debug(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Debug<T>(string messageTemplate, T propertyValue) => _clientLogger.Debug<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Debug<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Debug<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Debug<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Debug<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Debug(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Debug(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Debug(Exception? exception, string messageTemplate) => _clientLogger.Debug(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Debug<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Debug<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Debug<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Debug<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Debug<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Debug<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Debug(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Debug(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Information(string messageTemplate) => _clientLogger.Information(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Information<T>(string messageTemplate, T propertyValue) => _clientLogger.Information<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Information<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Information<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Information<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Information<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Information(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Information(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Information(Exception? exception, string messageTemplate) => _clientLogger.Information(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Information<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Information<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Information<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Information<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Information<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Information<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Information(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Information(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Warning(string messageTemplate) => _clientLogger.Warning(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Warning<T>(string messageTemplate, T propertyValue) => _clientLogger.Warning<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Warning<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Warning<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Warning<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Warning<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Warning(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Warning(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Warning(Exception? exception, string messageTemplate) => _clientLogger.Warning(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Warning<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Warning<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Warning<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Warning<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Warning<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Warning<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Warning(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Warning(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Error(string messageTemplate) => _clientLogger.Error(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Error<T>(string messageTemplate, T propertyValue) => _clientLogger.Error<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Error<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Error<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Error<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Error<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Error(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Error(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Error(Exception? exception, string messageTemplate) => _clientLogger.Error(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Error<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Error<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Error<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Error<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Error<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Error<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Error(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Error(exception, messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Fatal(string messageTemplate) => _clientLogger.Fatal(messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Fatal<T>(string messageTemplate, T propertyValue) => _clientLogger.Fatal<T>(messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Fatal<T0, T1>(string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Fatal<T0, T1>(messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Fatal<T0, T1, T2>(string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Fatal<T0, T1, T2>(messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Fatal(string messageTemplate, params object?[]? propertyValues) => _clientLogger.Fatal(messageTemplate, propertyValues);

    [Conditional("MOON_LOG")]
    public static void Fatal(Exception? exception, string messageTemplate) => _clientLogger.Fatal(exception, messageTemplate);

    [Conditional("MOON_LOG")]
    public static void Fatal<T>(Exception? exception, string messageTemplate, T propertyValue) => _clientLogger.Fatal<T>(exception, messageTemplate, propertyValue);

    [Conditional("MOON_LOG")]
    public static void Fatal<T0, T1>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1) => _clientLogger.Fatal<T0, T1>(exception, messageTemplate, propertyValue0, propertyValue1);

    [Conditional("MOON_LOG")]
    public static void Fatal<T0, T1, T2>(Exception? exception, string messageTemplate, T0 propertyValue0, T1 propertyValue1, T2 propertyValue2) => _clientLogger.Fatal<T0, T1, T2>(exception, messageTemplate, propertyValue0, propertyValue1, propertyValue2);

    [Conditional("MOON_LOG")]
    public static void Fatal(Exception? exception, string messageTemplate, params object?[]? propertyValues) => _clientLogger.Fatal(exception, messageTemplate, propertyValues);

    #endregion
}
