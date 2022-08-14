using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShoutgunCollectable : WeaponBase, ICollectible
{
    public Faction Faction => Faction.Player;
    public event Action<ICollectible> OnCollected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
