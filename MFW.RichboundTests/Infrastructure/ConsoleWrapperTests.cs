using JetBrains.Annotations;
using MFW.Richbound.Infrastructure;

namespace MFW.RichboundTests.Infrastructure;

[TestClass, UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class ConsoleWrapperTests
{
    private ConsoleWrapper _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _sut = new ConsoleWrapper();
    }

    [TestMethod]
    public void Clear_ShouldNotThrowException()
    {
        // Act & Assert
        _sut.Clear();
    }
}
