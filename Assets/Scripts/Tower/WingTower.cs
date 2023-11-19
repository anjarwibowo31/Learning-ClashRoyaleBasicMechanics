using System.Collections;
using UnityEngine;

public class WingTower : Tower
{
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
