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

        var actualSaveFileLines = File.ReadAllLines(SaveFile);

        // Assert
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
        var expectedGameStateDto = TestHelper.GetGameStateDto();

        _sut.SaveGame(expectedGameStateDto);

        // Act
        var actualGameStateDto = _sut.LoadGame();

        // Assert
        Assert.AreEqual(expectedGameStateDto, actualGameStateDto);
    }

    [TestMethod]
    public void LoadGame_WithoutSaveFile_ShouldReturnNull()
    {
        // Arrange
        _consoleLoggerMock
            .Setup(x => x.LogCritical(It.IsAny<string>(), Constants.DefaultLogFile))
            .Verifiable(Times.Once);

        // Act
        var actualGameStateDto = _sut.LoadGame();

        // Assert
        Assert.IsNull(actualGameStateDto);

        _consoleLoggerMock.Verify();
    }

    [TestMethod]
    public void HasSaveFile_WithSaveFile_ShouldReturnTrue()
    {
        // Arrange
        const bool expectedResult = true;

        _sut.SaveGame(TestHelper.GetGameStateDto());

        // Act
        var actualResult = _sut.HasSaveFile();

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }

    [TestMethod]
    public void HasSaveFile_WithoutSaveFile_ShouldReturnFalse()
    {
        // Arrange
        const bool expectedResult = false;

        // Act
        var actualResult = _sut.HasSaveFile();

        // Assert
        Assert.AreEqual(expectedResult, actualResult);
    }
}
