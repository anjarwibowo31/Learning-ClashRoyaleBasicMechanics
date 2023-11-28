using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public abstract float Health { get; }
    public abstract Participant Participant { get; }
    public void GetDamage(float damage);
    public Transform GetTransform();
}
