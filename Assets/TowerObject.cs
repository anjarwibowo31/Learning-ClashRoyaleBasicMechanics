using System;
using System.Collections.Generic;
using UnityEngine;

public class TowerObject : MonoBehaviour
{
    [SerializeField] private Tower tower;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private float attack;
    [SerializeField] private float attackRange;
    [SerializeField] private Projectile projectile;

    private Animator animator;
    private List<IDamageable> enemyDamageableList = new();
    private Transform target;
    private IDamageable targetScript;
    private Participant participant;
    private Participant oppositeParticipant;

    void Start()
    {
        participant = tower.Participant;

        if (participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else
        {
            oppositeParticipant = Participant.Player;
        }

        animator = GetComponent<Animator>();

        ParticipantDataManager.Instance.OnDamageableRemoved += ParticipantDataManager_OnDamageableListChanged;
        ParticipantDataManager.Instance.OnDamageableAdded += ParticipantDataManager_OnDamageableListChanged;

        enemyDamageableList = ParticipantDataManager.Instance.ParticipantDictionary[oppositeParticipant].DamageableList;
    }

    void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) < attackRange)
            {
                Vector3 targetWithoutY = new Vector3(target.position.x, transform.position.y, target.position.z);
                Vector3 direction = (targetWithoutY - transform.position).normalized;
                transform.forward = Vector3.Lerp(transform.forward, direction, Time.deltaTime * 5f);
                animator.SetBool("IsAttacking", true);
            }
            else
            {
                animator.SetBool("IsAttacking", false);
            }
        }
    }

    private void Attack()
    {
        Projectile projectileInstance = Instantiate(projectile, spawnPoint.position, Quaternion.identity);

        projectileInstance.SetProjectile(target, targetScript, attack);

        if (target == null) FindTarget();
    }

    private void ParticipantDataManager_OnDamageableListChanged(object sender, EventArgs e)
    {
        if (target == null) FindTarget();
    }

    private void FindTarget()
    {
        float nearestDistance = float.MaxValue;

        if (enemyDamageableList.Count > 0)
        {
            foreach (IDamageable damageable in enemyDamageableList)
            {
                //tower exception
                if (damageable.GetTransform().TryGetComponent(out Tower tower)) continue;

                Vector3 oppPos = damageable.GetTransform().position;
                float distance = Vector3.Distance(oppPos, transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    target = damageable.GetTransform();
                    targetScript = damageable;
                }
            }
        }
        else
        {
            target = null;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
