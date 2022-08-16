using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBase : MonoBehaviour
{
    [SerializeField] private float projectilePower;
    [SerializeField] private float ammoAmount;
    [SerializeField] private float cooldownBetweenFires;
}
