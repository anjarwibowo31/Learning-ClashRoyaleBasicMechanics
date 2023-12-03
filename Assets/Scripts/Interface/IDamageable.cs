using System;
using UnityEngine;

public interface IDamageable
{
    public class TowerDestroyedEventArgs : EventArgs
    {
        public Tower DestroyedTower { get; private set; }

        public TowerDestroyedEventArgs()
        {
            
        }

        public TowerDestroyedEventArgs(Tower destroyedTower)
        {
            DestroyedTower = destroyedTower;
        }
    }

    public event EventHandler<TowerDestroyedEventArgs> OnDamageableDestroyed;
    public event EventHandler OnDamageableDamaged;
    public float Health { get; }
    public Participant Participant { get; set; }
    public void GetDamage(float damage);
    public Transform GetTransform();
}
