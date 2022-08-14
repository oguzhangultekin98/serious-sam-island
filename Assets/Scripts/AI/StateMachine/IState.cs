public interface IState
{
    void Tick();
    void OnEnter();
    void OnExit();

    bool canTransitionItSelf { get; }
}