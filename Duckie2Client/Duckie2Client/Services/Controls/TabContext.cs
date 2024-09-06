namespace Duckie2Client.Services.Controls;

// Контекст определяет интерфейс, представляющий интерес для клиентов. Он также хранит ссылку на экземпляр подкласса
// Состояния, который отображает текущее состояние Контекста.
public class TabContext
{
    // Ссылка на текущее состояние Контекста.
    public TabState? CurrentState { get; set; }
    // private Dictionary<TabState, Func<bool>> Delegates = new();

    public TabContext(TabState? state)
    {
        // Set the initial state.
        SetState(state);
        CurrentState = state;
    }


    // Контекст позволяет изменять объект Состояния во время выполнения.
    public void SetState(TabState? state)
    {
        // Console.WriteLine($@"Context: Set state to {state?.GetType().Name}.");
        CurrentState?.EndState();
        CurrentState = state;
        CurrentState?.SetContext(this);
        CurrentState?.StartState();
    }

    // Контекст делегирует часть своего поведения текущему объекту Состояния.
}