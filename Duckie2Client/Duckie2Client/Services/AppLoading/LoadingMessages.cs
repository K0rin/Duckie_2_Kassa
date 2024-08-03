namespace Duckie2Client.Services.AppLoading;

public class LoadingMessages(
    string start,
    string goodStatus,
    string badStatus)
{
    public string Start => start;

    // todo: Надо ли использовать сообщение об успехе? Достаточно показать стартовое сообщение.
    public string GoodStatus => goodStatus;

    public string BadStatus => badStatus;
}