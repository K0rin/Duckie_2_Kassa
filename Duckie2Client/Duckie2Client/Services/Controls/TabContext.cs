using System;

namespace Duckie2Client.Services.Controls;

// Контекст определяет интерфейс, представляющий интерес для клиентов. Он также хранит ссылку на экземпляр подкласса
// Состояния, который отображает текущее состояние Контекста.
public class TabContext
{
    // Ссылка на текущее состояние Контекста.
    private TabState? _state;
    public TabState? CurrentState { get; set; }

    public TabContext(TabState? state)
    {
        // Set the initial state.
        SetState(state);
        CurrentState = state;
    }

    // Контекст позволяет изменять объект Состояния во время выполнения.
    public void SetState(TabState? state)
    {
        Console.WriteLine($@"Context: Set state to {state?.GetType().Name}.");
        _state?.EndState();
        _state = state;
        _state?.SetContext(this);
        _state?.StartState();
    }

    // Контекст делегирует часть своего поведения текущему объекту Состояния.
    public void Request()
    {
        _state?.Handle();
    }
}