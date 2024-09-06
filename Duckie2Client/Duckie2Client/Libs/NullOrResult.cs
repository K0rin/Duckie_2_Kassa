namespace Duckie2Client.Libs;

/// <summary>
/// <para>
/// The class is used as a replacement for null-value.
/// </para>
/// <para>
/// Used as a value returned by a function that contains a non-zero result of the function in the <c>Result</c>field.
/// If the function result is null, then the function returns an instance of this class without setting
/// the <c>Result</c> attribute.
/// </para>
/// <para>
/// The inheriting class must override the <c>IsNull</c>attribute so that it returns 'false'.
/// </para>
/// <para>
/// An example of creating 'non-null' result:
/// </para>
/// <example>
/// <code>
/// var result = new NullOrResult
/// {
///     Result = "Result of a function"
/// };
/// </code>
/// </example>
/// </summary>
public class NullOrResult
{
    /// <summary>
    /// The indicator shows whether a class treated as a 'null value' or as a 'non-null value'.
    /// </summary>
    public bool IsNull { get; private init; } = true;

    /// <summary>
    /// The result of a function. But not 'null'.
    /// </summary>
    private readonly object? _result;

    public object? Result
    {
        get => _result;
        init
        {
            // When setting a value to a field during object creation, change IsNull to 'true'.
            // Thus, a non-zero result will be indicated.
            _result = value;
            IsNull = false;
        }
    }
}