public interface IActionScheduler
{
    IActivatiableBase CurrentAction { get; }
    void StartAction<T>();
    void StartAction<T, T2>(T2 t2);
    void StopActiveAction();
}
