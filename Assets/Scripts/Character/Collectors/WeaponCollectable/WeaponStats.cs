using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/WeaponStat", order = 100)]
public class WeaponStats : ScriptableObject
{
    public bool isMelee;
    public float ammoAmount;
    public float cooldownBetweenFires;
    public ProjectileStats projectileStats;
    public GameObject projectile;
}
