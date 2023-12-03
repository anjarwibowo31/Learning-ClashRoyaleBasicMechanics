using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogLauncherObject : BaseSpawnObject
{
    [SerializeField] private float range;

    private float distanceTraveled = 0f;

    public override void SetPartyAndFlag(Participant participant)
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
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        distanceTraveled += moveSpeed * Time.deltaTime;

        if (distanceTraveled >= range)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.TryGetComponent<IDamageable>(out IDamageable damagable);

        if (damagable.Participant == oppositeParticipant)
        {
            damagable.GetDamage(attack);
        }
    }
}
