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

    [SerializeField] private float health;
    [SerializeField] private float attackRange;

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
            if (Vector3.Distance(transform.position, target.position) > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, Time.deltaTime * 5f);

                animator.SetBool("IsAttacking", false);
            }
            else
            {
                animator.SetBool("IsAttacking", true);
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
}
