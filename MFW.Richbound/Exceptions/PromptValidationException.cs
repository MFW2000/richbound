namespace MFW.Richbound.Exceptions;

/// <summary>
/// The exception that is thrown when a prompt validation error occurs.
/// </summary>
public class PromptValidationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PromptValidationException"/> class with a specified error message.
    /// </summary>
    /// <inheritdoc/>
    public PromptValidationException(string message) : base(message) { }
}
