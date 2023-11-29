using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TroopObject : BaseSpawnObject, IDamageable
{
    public float Health => health;

    public Participant Participant { get => participant; set => participant = value; }

    private Participant participant;
    private List<IDamageable> enemyDamageableList = new();
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

    public void SetPartyAndFlag(Participant participant)
    {
        Participant = participant;

        foreach (MeshRenderer meshRenderer in objectFlag)
        {
            meshRenderer.material = ParticipantDataManager.Instance.ParticipantDictionary[participant].partyFlag;
        }
    }


    private void Start()
    {
        if (Participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else if (Participant == Participant.Enemy)
        {
            oppositeParticipant = Participant.Player;
        }

        rb = GetComponent<Rigidbody>();

        ParticipantDataManager.Instance.AddDamageable(this, Participant);

        ParticipantDataManager.Instance.OnDamageableRemoved += ParticipantDataManager_OnDamageableRemoved;

        animator = GetComponent<Animator>();
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

        if (health <= 0)
        {
            GetDestroyed();
        }

        OnDamageableDamaged?.Invoke(this, EventArgs.Empty);
    }

    private void GetDestroyed()
    {
        ParticipantDataManager.Instance.OnDamageableRemoved -= ParticipantDataManager_OnDamageableRemoved;

        ParticipantDataManager.Instance.RemoveDamageable(this, Participant);

        OnDamageableDestroyed?.Invoke(this, new());
        Destroy(this.gameObject);
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
