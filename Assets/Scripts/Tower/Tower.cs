using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour, IDamageable
{
    public class TowerDestroyedEventArgs : EventArgs
    {
        public Tower DestroyedTower { get; private set; }

        public TowerDestroyedEventArgs(Tower destroyedTower)
        {
            DestroyedTower = destroyedTower;
        }
    }

    public event EventHandler<TowerDestroyedEventArgs> OnTowerDestroyed;
    public event EventHandler OnTowerDamaged;

    public float Health => health;
    public Participant Participant => participant;
    public GameObject TowerArea { get => towerArea; set => towerArea = value; }

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
    }

    public void Start()
    {
        foreach (MeshRenderer tower in towerFlag)
        {
            tower.material = ParticipantDataManager.Instance.ParticipantDictionary[participant].partyFlag;
        }
    }

    public virtual void GetDamage(float damageAmount)
    {
        health -= damageAmount;
        OnTowerDamaged?.Invoke(this, EventArgs.Empty);
        
        if (health <= 0)
        {
            GetDestroyed();
        }
    }

    public virtual void GetDestroyed()
    {
        ParticipantDataManager.Instance.RemoveDamageable(this, participant);
        TowerDestroyedEventArgs eventArgs = new(this);
        OnTowerDestroyed?.Invoke(this, eventArgs);

        towerVisual.SetActive(false);
        towerArea.SetActive(false);
        towerCollider.enabled = false;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
