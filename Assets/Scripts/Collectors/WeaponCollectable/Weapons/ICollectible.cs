using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollectible
{
    Faction Faction { get; }
    event Action<ICollectible> OnCollected;
}