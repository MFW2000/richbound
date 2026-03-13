using MFW.Richbound;
using MFW.Richbound.Enumerations;
using MFW.Richbound.Infrastructure;
using MFW.Richbound.Infrastructure.Interfaces;
using Moq;

namespace MFW.RichboundTests.Infrastructure;

[TestClass]
public class SaveFileManagerTests
{
    private const string SaveFile = Constants.DefaultSaveFile;

    private Mock<IConsoleLogger> _consoleLoggerMock = null!;

    private SaveFileManager _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _consoleLoggerMock = new Mock<IConsoleLogger>(MockBehavior.Strict);

        _sut = new SaveFileManager(_consoleLoggerMock.Object);
    }

    [TestCleanup]
    public void Cleanup()
    {
        if (File.Exists(SaveFile))
        {
            File.Delete(SaveFile);
        }
    }

    [TestMethod]
    public void SaveGame_ShouldSaveGameToFile()
    {
        // Arrange
        const Gender expectedGender = Gender.Male;
        const string expectedFirstName = "Ash";
        const string expectedLastName = "Ketchup";

        var gameStateDto = TestHelper.GetGameStateDto(
            gender: expectedGender,
            firstName: expectedFirstName,
            lastName: expectedLastName);

        // Act
        _sut.SaveGame(gameStateDto);

        // Assert
        var actualSaveFileLines = File.ReadAllLines(SaveFile);

        Assert.IsTrue(File.Exists(SaveFile));
        Assert.IsNotEmpty(actualSaveFileLines);
        Assert.Contains(line => line.Contains($"{(int)expectedGender}"), actualSaveFileLines);
        Assert.Contains(line => line.Contains(expectedFirstName), actualSaveFileLines);
        Assert.Contains(line => line.Contains(expectedLastName), actualSaveFileLines);
    }

    [TestMethod]
    public void LoadGame_ShouldLoadGameFromFile()
    {
        // Arrange
        var expected = TestHelper.GetGameStateDto();

        _sut.SaveGame(expected);

        // Act
        var actual = _sut.LoadGame();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void LoadGame_WithoutSaveFile_ShouldReturnNull()
    {
        // Arrange
        _consoleLoggerMock
            .Setup(x => x.LogCritical(It.IsAny<string>(), Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        // Act
        var actual = _sut.LoadGame();

        // Assert
        Assert.IsNull(actual);

        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    public void HasSaveFile_WithSaveFile_ShouldReturnTrue()
    {
        // Arrange
        const bool expected = true;

        _sut.SaveGame(TestHelper.GetGameStateDto());

        // Act
        var actual = _sut.HasSaveFile();

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod]
    public void HasSaveFile_WithoutSaveFile_ShouldReturnFalse()
    {
        // Arrange
        const bool expected = false;

        // Act
        var actual = _sut.HasSaveFile();

        // Assert
        Assert.AreEqual(expected, actual);
    }
}
