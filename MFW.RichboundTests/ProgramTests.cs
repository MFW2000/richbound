using JetBrains.Annotations;
using MFW.Richbound;

namespace MFW.RichboundTests;

[TestClass, UsedImplicitly(ImplicitUseTargetFlags.Members)]
public class ProgramTests
{
    [TestMethod]
    public void Main_ShouldOutputHelloWorld()
    {
        // Arrange
        const string expected = "Hello, World!";

        var consoleOutput = new StringWriter();

        Console.SetOut(consoleOutput);

        // Act
        Program.Main();

        // Assert
        var actual = consoleOutput.ToString();

        Assert.Contains(expected, actual);
    }
}
