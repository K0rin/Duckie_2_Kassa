namespace Duckie2Client.Libs.ErrorCollection;

public record ErrorInfo(int Number, string Message, string? Substitution = null)
{
    public int Number { get; set; } = Number;
    public string Message { get; set; } = Message;
    public string? Substitution { get; set; } = Substitution;
}