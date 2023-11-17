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

    public abstract float Health { get; set; }

    [SerializeField] protected GameObject towerVisual;
    [SerializeField] protected MeshRenderer[] towerFlag;
    [SerializeField] protected Participant participant;

    protected Collider towerCollider;

    public void Start()
    {
        foreach (MeshRenderer tower in towerFlag)
        {
            tower.material = ParticipantDataManager.Instance.participantDictionary[participant].partyFlag;
        }
    }

    private void Awake()
    {
        towerCollider = GetComponent<Collider>();
    }

    public virtual void GetDamage(float damageAmount)
    {
        Health -= damageAmount;
        OnTowerDamaged?.Invoke(this, EventArgs.Empty);
        
        if (Health <= 0)
        {
            GetDestroyed();
        }
    }

    public virtual void GetDestroyed()
    {
        TowerDestroyedEventArgs eventArgs = new TowerDestroyedEventArgs(this);
        OnTowerDestroyed?.Invoke(this, eventArgs);

        towerVisual.SetActive(false);
        towerCollider.enabled = false;
    }
}
