using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopObject : MonoBehaviour, IDamageable
{
    private Participant ownSide = Participant.Player;
    private Participant opposite;

    private List<IDamageable> enemyDamageableList = new();

    private SingleParticipantData thisParticipantData;

    private Transform target;

    public Participant Participant => throw new System.NotImplementedException();

    private void Awake()
    {
        if (ownSide == Participant.Player)
        {
            opposite = Participant.Enemy;
        }
        else
        {
            opposite = Participant.Player;
        }

        ParticipantDataManager.Instance.ParticipantDictionary[ownSide].DamageableList.Add(this);

        thisParticipantData = ParticipantDataManager.Instance.ParticipantDictionary[ownSide];
    }

    private void Start()
    {
        ParticipantDataManager.Instance.OnDamageableRemoved += ParticipantDataManager_OnDamageableRemoved;
        enemyDamageableList = ParticipantDataManager.Instance.ParticipantDictionary[opposite].DamageableList;

        // dapatkan semua data game object dari musuh
        // kalkulasi game object dari musuh
        // Harus ada tag opposite dan ownSide untuk mencegah ally damage

        target = FindTarget();
    }

    private void Update()
    {
    }

    public void ParticipantDataManager_OnDamageableRemoved(object sender, System.EventArgs e)
    {
        target = FindTarget();
    }

    private Transform FindTarget()
    {
        float nearestDistance = float.MaxValue;

        foreach (IDamageable damageable in enemyDamageableList)
        {
            Vector3 oppPos = damageable.GetTransform().position;
            float distance = Vector3.Distance(oppPos, transform.position);
            if (distance < nearestDistance)
            {
                nearestDistance = distance;
                return damageable.GetTransform();
            }
        }
        return null;
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
