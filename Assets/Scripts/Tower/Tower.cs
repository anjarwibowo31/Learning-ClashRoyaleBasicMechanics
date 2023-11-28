using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IDamageable
{
    public float Health => health;
    public Participant Participant => participant;
    public GameObject TowerArea { get => towerArea; set => towerArea = value; }

    [SerializeField] private float health;
    [SerializeField] private GameObject towerVisual;
    [SerializeField] private GameObject towerArea;
    [SerializeField] private MeshRenderer[] towerFlag;
    [SerializeField] private Participant participant;

    protected Collider towerCollider;

    public event EventHandler<IDamageable.TowerDestroyedEventArgs> OnDamageableDestroyed;
    public event EventHandler OnDamageableDamaged;

    private void Awake()
    {
        towerCollider = GetComponent<Collider>();

        ParticipantDataManager.Instance.ParticipantDictionary[participant].TowerList.Add(this);
        ParticipantDataManager.Instance.ParticipantDictionary[participant].RestrictionAreaList.Add(towerArea);

        ParticipantDataManager.Instance.AddDamageable(this, participant);


    }

    public void Start()
    {
        foreach (MeshRenderer tower in towerFlag)
        {
            tower.material = ParticipantDataManager.Instance.ParticipantDictionary[participant].partyFlag;
        }

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
        ParticipantDataManager.Instance.RemoveDamageable(this, participant);
        IDamageable.TowerDestroyedEventArgs eventArgs = new(this);
        OnDamageableDestroyed?.Invoke(this, eventArgs);

        towerVisual.SetActive(false);
        towerArea.SetActive(false);
        towerCollider.enabled = false;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
