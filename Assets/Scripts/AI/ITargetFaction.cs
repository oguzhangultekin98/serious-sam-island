using System;
using UnityEngine;

public interface IDamageable
{
    bool IsAlive { get; }
    Vector3 Pos { get; }
    void ApplyDamage(int damage = 1);
}

public interface ITargetFaction : IDamageable
{
    Faction Faction { get; }

    event Action<ITargetFaction> OnRemoved;
}
