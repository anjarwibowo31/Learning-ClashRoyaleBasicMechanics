using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTower : Tower
{
    [SerializeField] private float healthValue = 2000;

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

        // implement absolute defeated jika main tower hancur
    }
}
