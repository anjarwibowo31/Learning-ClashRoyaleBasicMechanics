using System.Collections;
using UnityEngine;

public class WingTower : Tower
{
    [SerializeField] private float healthValue = 1500;

    public override float Health
    {
        get
        {
            return healthValue;
        }
        set
        {
            healthValue = value;
        }
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();

        // aktifkan main tower defensive canon
    }
}
