namespace Duckie2Client.Libs;

public class LoadingMessages(
    string start,
    string goodStatus,
    string badStatus)
{
    public string Start => start;

    public string GoodStatus => goodStatus;

    public string BadStatus => badStatus;
}