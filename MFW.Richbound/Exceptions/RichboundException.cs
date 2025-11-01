namespace MFW.Richbound.Exceptions;

/// <summary>
/// The exception that is thrown when a Richbound specific exception occurs.
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
    /// <inheritdoc/>
    public RichboundException(string message) : base(message) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="RichboundException"/> class with a specified error message and a
    /// reference to the inner exception that is the cause of this exception.
    /// </summary>
    /// <inheritdoc/>
    public RichboundException(string message, Exception innerException) : base(message, innerException) { }
}
