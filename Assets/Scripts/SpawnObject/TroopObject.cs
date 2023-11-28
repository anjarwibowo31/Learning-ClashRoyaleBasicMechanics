using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TroopObject : BaseSpawnObject, IDamageable
{
    public float Health => health;

    private List<IDamageable> enemyDamageableList = new();
    private SingleParticipantData thisParticipantData;
    private Transform target;
    private IDamageable targetScript;
    private Rigidbody rb;
    private Participant oppositeParticipant;
    private Animator animator;
    private bool isMoving = true;

    [SerializeField] private float health;
    [SerializeField] private float attackRange;

    public event EventHandler<IDamageable.TowerDestroyedEventArgs> OnDamageableDestroyed;
    public event EventHandler OnDamageableDamaged;

    private void Awake()
    {
        if (Participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else
        {
            oppositeParticipant = Participant.Player;
        }

        ParticipantDataManager.Instance.ParticipantDictionary[Participant].DamageableList.Add(this);

        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        ParticipantDataManager.Instance.OnDamageableRemoved += ParticipantDataManager_OnDamageableRemoved;
        enemyDamageableList = ParticipantDataManager.Instance.ParticipantDictionary[oppositeParticipant].DamageableList;

        FindTarget();
    }

    private void Update()
    {
        rb.velocity = Vector3.zero;

        if (target != null)
        {
            Vector3 moveDirection = (target.position - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 5f);

            foreach (Collider collider in Physics.OverlapSphere(transform.position, attackRange))
            {
                if (collider.transform == target.transform)
                {
                    isMoving = false;
                    animator.SetBool("IsAttacking", true);
                    return;
                }
                else if (target != null)
                {
                    isMoving = true;
                }
            }

            if (isMoving)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);

                animator.SetBool("IsAttacking", false);
            }
        }
    }

    public void ParticipantDataManager_OnDamageableRemoved(object sender, System.EventArgs e)
    {
        animator.SetBool("IsAttacking", false);
        FindTarget();
    }

    private void FindTarget()
    {
        float nearestDistance = float.MaxValue;

        if (enemyDamageableList.Count > 0)
        {
            foreach (IDamageable damageable in enemyDamageableList)
            {
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

    public void GetDamage(float damage)
    {
        health -= damage;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    // Animation Event
    public void Attack()
    {
        targetScript.GetDamage(attack);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
    }
}
