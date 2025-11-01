using MFW.Richbound;

namespace MFW.RichboundTests;

[TestClass]
public class ProgramTests
{
    [TestMethod]
    public void Main_ShouldOutputHelloWorld()
    {
        // Arrange
        var consoleOutput = new StringWriter();

        Console.SetOut(consoleOutput);

        // Act
        Program.Main();

        // Assert
        Assert.IsTrue(consoleOutput.ToString().Contains("Hello, World!"));
    }
}
