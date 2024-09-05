using System;

namespace Duckie2Client.Services.Controls;

// Базовый класс Состояния объявляет методы, которые должны реализовать все Конкретные Состояния, а также предоставляет
// обратную ссылку на объект Контекст, связанный с Состоянием. Эта обратная ссылка может использоваться Состояниями для
// передачи Контекста другому Состоянию.
public abstract class TabState
{
    protected TabContext? Context;

    public void SetContext(TabContext? context)
    {
        Context = context;
    }

    /// <summary>
    /// Performs operations with the current context before switching to another state.
    /// </summary>
    public void EndState()
    {
        Console.WriteLine(@$"hide controls : {GetType().Name}");
    }

    /// <summary>
    /// Performs operations with the current context after switching it.
    /// </summary>
    public void StartState()
    {
        Console.WriteLine(@$"show controls : {GetType().Name}");
        Handle();
    }

    public abstract void Handle();
}