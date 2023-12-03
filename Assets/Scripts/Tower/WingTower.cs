using System;

public class WingTower : Tower
{
    public event Action OnWingTowerDestroyed;

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();

        OnWingTowerDestroyed?.Invoke();
    }
}
