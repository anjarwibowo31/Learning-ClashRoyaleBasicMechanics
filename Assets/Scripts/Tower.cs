using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDestroyedEventArgs : EventArgs
{
    public Tower DestroyedTower { get; private set; }

    public TowerDestroyedEventArgs(Tower destroyedTower)
    {
        DestroyedTower = destroyedTower;
    }
}

public abstract class Tower : MonoBehaviour
{
    public event EventHandler<TowerDestroyedEventArgs> OnTowerDestroyed;
    public event EventHandler OnTowerDamaged;

    public abstract float Health { get; set; }

    [SerializeField] protected GameObject towerVisual;

    protected Collider towerCollider; 

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