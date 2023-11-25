using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopObject : MonoBehaviour, IDamageable
{
    public Participant Participant => throw new System.NotImplementedException();

    private Participant participant = Participant.Player;
    private Participant oppositeParticipant;

    private List<IDamageable> enemyDamageableList = new();

    private SingleParticipantData thisParticipantData;

    private Transform target;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float shootingRange;

    private void Awake()
    {
        if (participant == Participant.Player)
        {
            oppositeParticipant = Participant.Enemy;
        }
        else
        {
            oppositeParticipant = Participant.Player;
        }

        ParticipantDataManager.Instance.ParticipantDictionary[participant].DamageableList.Add(this);

        thisParticipantData = ParticipantDataManager.Instance.ParticipantDictionary[participant];
    }

    private void Start()
    {
        ParticipantDataManager.Instance.OnDamageableRemoved += ParticipantDataManager_OnDamageableRemoved;
        enemyDamageableList = ParticipantDataManager.Instance.ParticipantDictionary[oppositeParticipant].DamageableList;

        FindTarget();
    }

    private void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.position) > shootingRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
            }
            else
            {
                // instantiate object dan tembak ke target
            }
        }
    }
    public void ParticipantDataManager_OnDamageableRemoved(object sender, System.EventArgs e)
    {
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
                }
            }
        }
    }

    public void GetDamage(float damage)
    {
        throw new System.NotImplementedException();
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
