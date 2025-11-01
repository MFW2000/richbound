using JetBrains.Annotations;
using MFW.Richbound;

namespace MFW.RichboundTests;

[TestClass, UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class RunnerTests
{
    private Runner _sut = null!;

    [TestInitialize]
    public void Setup()
    {
        _sut = new Runner();
    }

    [TestMethod]
    public void Run_ShouldOutputHelloWorld()
    {
        // Arrange
        const string expected = "Hello, World!";

        var consoleOutput = new StringWriter();

        Console.SetOut(consoleOutput);

        // Act
        _sut.Run();

        // Assert
        var actual = consoleOutput.ToString();

        Assert.Contains(expected, actual);
    }
}
