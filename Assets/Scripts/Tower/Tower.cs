using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tower : MonoBehaviour
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

    public float Health { get { return health; } }
    public Participant Participant { get { return participant; } }
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
    }

    public void Start()
    {
        ParticipantDataManager.Instance.ParticipantDictionary[this.participant].TowerList.Add(this);
        ParticipantDataManager.Instance.ParticipantDictionary[this.participant].RestrictionArea.Add(towerArea);

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
        TowerDestroyedEventArgs eventArgs = new TowerDestroyedEventArgs(this);
        OnTowerDestroyed?.Invoke(this, eventArgs);

        towerVisual.SetActive(false);
        towerArea.SetActive(false);
        towerCollider.enabled = false;
    }
}
