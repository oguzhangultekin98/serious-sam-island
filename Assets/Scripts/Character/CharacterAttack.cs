using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttack : MonoBehaviour
{
    private Eye characterEye;
    private float lastAttacked = 0.0f;
    [SerializeField]private WeaponStats weaponStats;


    private void Start()
    {
        characterEye = GetComponentInChildren<Eye>();
    }

    private void Update()
    {
        lastAttacked += Time.deltaTime;
        bool canAttackAgain = lastAttacked > weaponStats.cooldownBetweenFires;
        if (canAttackAgain)
        {
            lastAttacked = 0f;
            var closestOtherFaction = characterEye.GetClosestTargetOfFaction(Faction.Enemy);
            if (closestOtherFaction != null)
            {
                Attack(closestOtherFaction);
            }
        }
    }

    private void Attack(ITargetFaction target)
    {
        var projectile = Instantiate(weaponStats.projectile,transform.position,Quaternion.identity).GetComponent<Projectile>();//Projectile init

        Vector3 enemyPos = target.Pos + Vector3.up;

        Vector3 bulletDir = (enemyPos - transform.position).normalized;
        var targetFaction = Faction.Enemy;

        projectile.Init(bulletDir, targetFaction, weaponStats.projectileStats);
        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
    }

}
