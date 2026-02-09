using System.Reflection;
using MFW.Richbound.Providers;
using Moq;

namespace MFW.RichboundTests.Providers;

[TestClass]
public class AssemblyVersionProviderTests
{
    private Mock<Assembly> _assemblyMock = null!;

    private AssemblyVersionProvider _sut = null!;

    [TestInitialize]
    public void Initialize()
    {
        _assemblyMock = new Mock<Assembly>(MockBehavior.Strict);

        _sut = new AssemblyVersionProvider(_assemblyMock.Object);
    }

    [TestMethod]
    public void GetVersion_WithFoundVersion_ReturnsVersion()
    {
        // Arrange
        var expected = new Version(1, 2, 3);

        var assemblyName = new AssemblyName
        {
            Version = expected
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actual = _sut.GetVersion();

        // Assert
        Assert.AreEqual(expected, actual);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetVersion_WithVersionNotFound_ReturnsNull()
    {
        // Arrange
        Version? expected = null;

        var assemblyName = new AssemblyName
        {
            Version = expected
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actual = _sut.GetVersion();

        // Assert
        Assert.AreEqual(expected, actual);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetFormattedVersion_WithFoundVersion_ReturnsString()
    {
        // Arrange
        const string expected = "1.2.3";

        var assemblyName = new AssemblyName
        {
            Version = new Version(1, 2, 3)
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actual = _sut.GetFormattedVersion();

        // Assert
        Assert.AreEqual(expected, actual);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetFormattedVersion_WithVersionNotFound_ReturnsEmptyString()
    {
        // Arrange
        var expected = string.Empty;

        var assemblyName = new AssemblyName
        {
            Version = null
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actual = _sut.GetFormattedVersion();

        // Assert
        Assert.AreEqual(expected, actual);

        _assemblyMock.Verify();
    }
}
