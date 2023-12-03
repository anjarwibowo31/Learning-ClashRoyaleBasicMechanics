using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IDamageable
{
    public event EventHandler<IDamageable.TowerDestroyedEventArgs> OnDamageableDestroyed;
    public event EventHandler OnDamageableDamaged;

    public float Health => health;
    public GameObject TowerArea { get => towerArea; set => towerArea = value; }
    public Participant Participant { get => participant; set => participant = value; }

    [SerializeField] private float health;
    [SerializeField] private GameObject towerVisual;
    [SerializeField] private GameObject towerArea;
    [SerializeField] private MeshRenderer[] towerFlag;
    [SerializeField] private Participant participant;

    protected Collider towerCollider;

    private void Awake()
    {
        towerCollider = GetComponent<Collider>();

        ParticipantDataManager.Instance.ParticipantDictionary[participant].TowerList.Add(this);
        ParticipantDataManager.Instance.ParticipantDictionary[participant].RestrictionAreaList.Add(towerArea);

        ParticipantDataManager.Instance.AddDamageable(this, participant);

        SetPartyAndFlag(participant);
    }

    protected virtual void Start()
    {
        OnDamageableDestroyed += GameplaySystem.Instance.Tower_OnTowerDestroyed;

    }

    public virtual void GetDamage(float damageAmount)
    {
        health -= damageAmount;
        OnDamageableDamaged?.Invoke(this, EventArgs.Empty);
        
        if (health <= 0)
        {
            GetDestroyed();
        }
    }

    public virtual void GetDestroyed()
    {
        ParticipantDataManager.Instance.ParticipantDictionary[participant].RestrictionAreaList.Remove(towerArea);
        ParticipantDataManager.Instance.RemoveDamageable(this, participant);
        OnDamageableDestroyed?.Invoke(this, new(this));

        towerVisual.SetActive(false);
        towerArea.SetActive(false);
        towerCollider.enabled = false;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetPartyAndFlag(Participant participant)
    {
        foreach (MeshRenderer tower in towerFlag)
        {
            tower.material = ParticipantDataManager.Instance.ParticipantDictionary[participant].partyFlag;
        }
    }
}
