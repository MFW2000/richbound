namespace MFW.Richbound.Exceptions;

/// <summary>
/// The exception that is thrown when a custom application-specific exception occurs.
/// </summary>
public class RichboundException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RichboundException"/> class.
    /// </summary>
    public RichboundException() { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RichboundException"/> class with a specified error message.
    /// </summary>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public RichboundException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RichboundException"/> class with a specified error message and the
    /// exception that is the cause of this exception.
    /// </summary>
    /// <param name="message">The error message that explains the reason for this exception.</param>
    /// <param name="innerException">
    /// The exception that is the cause of the current exception or a null reference (Nothing in Visual Basic) if no
    /// inner exception is specified.
    /// </param>
    public RichboundException(string message, Exception innerException) : base(message, innerException) { }
}
