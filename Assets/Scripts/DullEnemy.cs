using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DullEnemy : MonoBehaviour, ITargetFaction
{
    public Faction Faction => Faction.Enemy;

    public bool IsAlive => true;

    public Vector3 Pos => transform.position;

    public event Action<ITargetFaction> OnRemoved;

    public void ApplyDamage(int damage = 1)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
