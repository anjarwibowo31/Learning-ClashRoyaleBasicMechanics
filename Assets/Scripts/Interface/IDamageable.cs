using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public class TowerDestroyedEventArgs : EventArgs
    {
        public Tower DestroyedTower { get; private set; }

        public TowerDestroyedEventArgs(Tower destroyedTower)
        {
            DestroyedTower = destroyedTower;
        }
    }

    public event EventHandler<TowerDestroyedEventArgs> OnDamageableDestroyed;
    public event EventHandler OnDamageableDamaged;
    public abstract float Health { get; }
    public abstract Participant Participant { get; }
    public void GetDamage(float damage);
    public Transform GetTransform();
}
