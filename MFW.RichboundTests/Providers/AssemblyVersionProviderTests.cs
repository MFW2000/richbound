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
        var expectedVersion = new Version(1, 2, 3);

        var assemblyName = new AssemblyName
        {
            Version = expectedVersion
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actualVersion = _sut.GetVersion();

        // Assert
        Assert.AreEqual(expectedVersion, actualVersion);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetVersion_WithVersionNotFound_ReturnsNull()
    {
        // Arrange
        Version? expectedVersion = null;

        var assemblyName = new AssemblyName
        {
            Version = expectedVersion
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actualVersion = _sut.GetVersion();

        // Assert
        Assert.IsNull(actualVersion);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetFormattedVersion_WithFoundVersion_ReturnsString()
    {
        // Arrange
        const string expectedVersion = "1.2.3";

        var version = new Version(1, 2, 3);

        var assemblyName = new AssemblyName
        {
            Version = version
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actualVersion = _sut.GetFormattedVersion();

        // Assert
        Assert.AreEqual(expectedVersion, actualVersion);

        _assemblyMock.Verify();
    }

    [TestMethod]
    public void GetFormattedVersion_WithVersionNotFound_ReturnsEmptyString()
    {
        // Arrange
        var expectedVersion = string.Empty;

        Version? version = null;

        var assemblyName = new AssemblyName
        {
            Version = version
        };

        _assemblyMock
            .Setup(a => a.GetName())
            .Returns(assemblyName)
            .Verifiable(Times.Once);

        // Act
        var actualVersion = _sut.GetFormattedVersion();

        // Assert
        Assert.AreEqual(expectedVersion, actualVersion);

        _assemblyMock.Verify();
    }
}
