using MFW.Richbound.Infrastructure;
using Microsoft.Extensions.Time.Testing;

namespace MFW.RichboundTests.Infrastructure;

[TestClass]
public class ConsoleLoggerTests
{
    private readonly DateTimeOffset _fakeTime = new(2025, 12, 20, 23, 0, 0, TimeSpan.Zero);
    private string _logFile = string.Empty;

    private FakeTimeProvider _fakeTimeProvider = null!;

    private ConsoleLogger _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _fakeTimeProvider = new FakeTimeProvider(_fakeTime);

        _sut = new ConsoleLogger(_fakeTimeProvider);

        _logFile = $"{Guid.NewGuid()}.log";
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_logFile))
        {
            File.Delete(_logFile);
        }
    }

    [TestMethod]
    public void LogDebug_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "DBG";
        const string message = "Debug test message.";

        var expected = GetExpectedLogLine(_fakeTime, logLevel, message);

        // Act
        _sut.LogDebug(message, _logFile);

        // Assert
        var logFileLines = File.ReadAllLines(_logFile);

        Assert.HasCount(1, logFileLines);
        Assert.AreEqual(expected, logFileLines[0]);
    }

    [TestMethod]
    public void LogInformation_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "INF";
        const string message = "Information test message.";

        var expected = GetExpectedLogLine(_fakeTime, logLevel, message);

        // Act
        _sut.LogInformation(message, _logFile);

        // Assert
        var logFileLines = File.ReadAllLines(_logFile);

        Assert.HasCount(1, logFileLines);
        Assert.AreEqual(expected, logFileLines[0]);
    }

    [TestMethod]
    public void LogWarning_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "WRN";
        const string message = "Warning test message.";

        var expected = GetExpectedLogLine(_fakeTime, logLevel, message);

        // Act
        _sut.LogWarning(message, _logFile);

        // Assert
        var logFileLines = File.ReadAllLines(_logFile);

        Assert.HasCount(1, logFileLines);
        Assert.AreEqual(expected, logFileLines[0]);
    }

    [TestMethod]
    public void LogError_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "ERR";
        const string message = "Error test message.";

        var expected = GetExpectedLogLine(_fakeTime, logLevel, message);

        // Act
        _sut.LogError(message, _logFile);

        // Assert
        var logFileLines = File.ReadAllLines(_logFile);

        Assert.HasCount(1, logFileLines);
        Assert.AreEqual(expected, logFileLines[0]);
    }

    [TestMethod]
    public void LogCritical_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "FTL";
        const string message = "Critical test message.";

        var expected = GetExpectedLogLine(_fakeTime, logLevel, message);

        // Act
        _sut.LogCritical(message, _logFile);

        // Assert
        var logFileLines = File.ReadAllLines(_logFile);

        Assert.HasCount(1, logFileLines);
        Assert.AreEqual(expected, logFileLines[0]);
    }

    [TestMethod]
    public void LogDebug_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogDebug(message, _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogInformation_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogInformation(message, _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogWarning_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogWarning(message, _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogError_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogError(message, _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogCritical_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogCritical(message, _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogDebug_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _logFile = string.Empty;

        // Act
        _sut.LogDebug("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogInformation_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _logFile = string.Empty;

        // Act
        _sut.LogInformation("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogWarning_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _logFile = string.Empty;

        // Act
        _sut.LogWarning("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogError_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _logFile = string.Empty;

        // Act
        _sut.LogError("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogCritical_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _logFile = string.Empty;

        // Act
        _sut.LogCritical("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogDebug_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _logFile = "invalid.txt";

        // Act
        _sut.LogDebug("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogInformation_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _logFile = "invalid.txt";

        // Act
        _sut.LogInformation("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogWarning_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _logFile = "invalid.txt";

        // Act
        _sut.LogWarning("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogError_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _logFile = "invalid.txt";

        // Act
        _sut.LogError("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogCritical_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _logFile = "invalid.txt";

        // Act
        _sut.LogCritical("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogDebug_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _logFile = ".log";

        // Act
        _sut.LogDebug("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogInformation_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _logFile = ".log";

        // Act
        _sut.LogInformation("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogWarning_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _logFile = ".log";

        // Act
        _sut.LogWarning("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogError_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _logFile = ".log";

        // Act
        _sut.LogError("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    [TestMethod]
    public void LogCritical_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _logFile = ".log";

        // Act
        _sut.LogCritical("Test", _logFile);

        // Assert
        Assert.IsFalse(File.Exists(_logFile));
    }

    private static string GetExpectedLogLine(DateTimeOffset timestamp, string logLevelString, string message)
    {
        return $"[{timestamp:yyyy-MM-dd HH:mm:ss} {logLevelString}] {message}";
    }
}
