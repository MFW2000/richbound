using MFW.Richbound.Infrastructure;
using Microsoft.Extensions.Time.Testing;

namespace MFW.RichboundTests.Infrastructure;

[TestClass]
public class ConsoleLoggerTests
{
    private string _testLogFile = string.Empty;
    private readonly DateTimeOffset _fakeTime = new(2025, 12, 20, 23, 0, 0, TimeSpan.Zero);

    private FakeTimeProvider _fakeTimeProvider = null!;

    private ConsoleLogger _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _fakeTimeProvider = new FakeTimeProvider(_fakeTime);

        _sut = new ConsoleLogger(_fakeTimeProvider);

        _testLogFile = $"{Guid.NewGuid()}.log";
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(_testLogFile))
        {
            File.Delete(_testLogFile);
        }
    }

    [TestMethod]
    public void LogDebug_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "DBG";
        const string message = "Debug test message.";

        var expectedMessage = GetExpectedMessage(_fakeTime, logLevel, message);

        // Act
        _sut.LogDebug(message, _testLogFile);

        // Assert
        var actualFileContent = File.ReadAllLines(_testLogFile);

        Assert.HasCount(1, actualFileContent);
        Assert.AreEqual(expectedMessage, actualFileContent[0]);
    }

    [TestMethod]
    public void LogInformation_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "INF";
        const string message = "Information test message.";

        var expectedMessage = GetExpectedMessage(_fakeTime, logLevel, message);

        // Act
        _sut.LogInformation(message, _testLogFile);

        // Assert
        var actualFileContent = File.ReadAllLines(_testLogFile);

        Assert.HasCount(1, actualFileContent);
        Assert.AreEqual(expectedMessage, actualFileContent[0]);
    }

    [TestMethod]
    public void LogWarning_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "WRN";
        const string message = "Warning test message.";

        var expectedMessage = GetExpectedMessage(_fakeTime, logLevel, message);

        // Act
        _sut.LogWarning(message, _testLogFile);

        // Assert
        var actualFileContent = File.ReadAllLines(_testLogFile);

        Assert.HasCount(1, actualFileContent);
        Assert.AreEqual(expectedMessage, actualFileContent[0]);
    }

    [TestMethod]
    public void LogError_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "ERR";
        const string message = "Error test message.";

        var expectedMessage = GetExpectedMessage(_fakeTime, logLevel, message);

        // Act
        _sut.LogError(message, _testLogFile);

        // Assert
        var actualFileContent = File.ReadAllLines(_testLogFile);

        Assert.HasCount(1, actualFileContent);
        Assert.AreEqual(expectedMessage, actualFileContent[0]);
    }

    [TestMethod]
    public void LogCritical_ShouldLogToLogFile()
    {
        // Arrange
        const string logLevel = "FTL";
        const string message = "Critical test message.";

        var expectedMessage = GetExpectedMessage(_fakeTime, logLevel, message);

        // Act
        _sut.LogCritical(message, _testLogFile);

        // Assert
        var actualFileContent = File.ReadAllLines(_testLogFile);

        Assert.HasCount(1, actualFileContent);
        Assert.AreEqual(expectedMessage, actualFileContent[0]);
    }

    [TestMethod]
    public void LogDebug_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogDebug(message, _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogInformation_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogInformation(message, _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogWarning_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogWarning(message, _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogError_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogError(message, _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogCritical_WithEmptyMessage_ShouldSkipLogging()
    {
        // Arrange
        var message = string.Empty;

        // Act
        _sut.LogError(message, _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogDebug_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = string.Empty;

        // Act
        _sut.LogDebug("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogInformation_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = string.Empty;

        // Act
        _sut.LogInformation("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogWarning_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = string.Empty;

        // Act
        _sut.LogWarning("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogError_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = string.Empty;

        // Act
        _sut.LogError("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogCritical_WithEmptyLogFile_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = string.Empty;

        // Act
        _sut.LogError("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogDebug_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = "invalid.txt";

        // Act
        _sut.LogDebug("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogInformation_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = "invalid.txt";

        // Act
        _sut.LogInformation("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogWarning_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = "invalid.txt";

        // Act
        _sut.LogWarning("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogError_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = "invalid.txt";

        // Act
        _sut.LogError("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogCritical_WithInvalidLogFileExtension_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = "invalid.txt";

        // Act
        _sut.LogCritical("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogDebug_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = ".log";

        // Act
        _sut.LogDebug("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogInformation_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = ".log";

        // Act
        _sut.LogInformation("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogWarning_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = ".log";

        // Act
        _sut.LogWarning("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogError_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = ".log";

        // Act
        _sut.LogError("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    [TestMethod]
    public void LogCritical_WithInvalidLogFileName_ShouldSkipLogging()
    {
        // Arrange
        _testLogFile = ".log";

        // Act
        _sut.LogCritical("Test", _testLogFile);

        // Assert
        Assert.IsFalse(File.Exists(_testLogFile));
    }

    private static string GetExpectedMessage(DateTimeOffset timestamp, string logLevelString, string message)
    {
        return $"[{timestamp:yyyy-MM-dd HH:mm:ss} {logLevelString}] {message}";
    }
}
