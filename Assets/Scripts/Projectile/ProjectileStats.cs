using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ProjectileStat", order = 100)]
public class ProjectileStats : ScriptableObject
{
    public float power;
    public float projectileSpeed;
}
