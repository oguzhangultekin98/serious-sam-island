using UnityEngine;

public abstract class ActionSchedulerBase : MonoBehaviour, IActionScheduler
{
    public IActivatiableBase CurrentAction { get; private set; }

    public void StartAction<T>()
    {
        if (CurrentAction is T) return;
        CurrentAction?.Deactivate();

        var action = GetComponentInChildren<T>() as IAction;

        action?.Activate();

        CurrentAction = action;
    }

    public void StartAction<T, T2>(T2 t2)
    {
        if (CurrentAction is T) return;
        CurrentAction?.Deactivate();

        var action = GetComponentInChildren<T>() as IAction<T2>;

        action?.Activate(t2);

        CurrentAction = action;
    }

    public void StopActiveAction()
    {
        CurrentAction.Deactivate();
        CurrentAction = null;
    }

    public abstract void StartDefaultActions();
}
