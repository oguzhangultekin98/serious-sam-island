public interface IActivatiableBase
{
    public bool IsActive { get; }

    public void Deactivate();
}

public interface IActivatiable : IActivatiableBase
{
    public void Activate();
}

public interface IActivatiable<T> : IActivatiableBase
{
    public void Activate(in T data);
}
