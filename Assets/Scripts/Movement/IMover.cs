using UnityEngine;

public interface IMover<in T, T2>
{
    T2 MovementComponent { get; }

    void MoveTo(T data);
}