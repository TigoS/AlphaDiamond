using System.Runtime.Serialization;
using DiamondCreatorLib.Interfaces;

namespace DiamondCreatorLib.Utils;

/// <summary>
///     Describes exception raised when an error occurred in <see cref="IDiamondCreatorService"/> service.
/// </summary>
public class DiamondCreatorException : Exception, ISerializable
{
    /// <inheritdoc />
    public DiamondCreatorException(string message)
        : base(string.Format(Properties.Resources.DIAMOND_CREATOR_EXCEPTION_PATTERN, message))
    {
    }

    /// <inheritdoc />
    public DiamondCreatorException(string format, params object[] args)
        : this(string.Format(format, args))
    {
    }

    /// <inheritdoc />
    public DiamondCreatorException(string message, Exception innerException)
        : base(string.Format(Properties.Resources.DIAMOND_CREATOR_EXCEPTION_PATTERN, message), innerException)
    {
    }

    /// <inheritdoc />
    public DiamondCreatorException(Exception innerException, string format, params object[] args)
        : this(string.Format(format, args), innerException)
    {
    }

    /// <inheritdoc />
    protected DiamondCreatorException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }

    /// <inheritdoc />
    void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
    {
        base.GetObjectData(info, context);
    }
}
