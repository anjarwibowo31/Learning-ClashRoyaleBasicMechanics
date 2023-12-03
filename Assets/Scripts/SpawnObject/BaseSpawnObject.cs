using UnityEngine;


public abstract class BaseSpawnObject : MonoBehaviour
{
    public Participant Participant { get => participant; set => participant = value; }

    [SerializeField] private Participant participant;

    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float attack;
    [SerializeField] protected MeshRenderer[] objectFlag;

    protected Participant oppositeParticipant;

    public abstract void SetPartyAndFlag(Participant participant);
}
