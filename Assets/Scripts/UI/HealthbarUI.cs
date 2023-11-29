using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    private Slider healthbar;

    private IDamageable damageableObject;

    private void Start()
    {
        damageableObject = GetComponentInParent<IDamageable>();
        healthbar = GetComponentInChildren<Slider>();

        damageableObject.OnDamageableDamaged += DamageableObject_OnDamageableDamaged; ;
        damageableObject.OnDamageableDestroyed += DamageableObject_OnDamageableDestroyed;

        healthbar.gameObject.SetActive(false);
        healthbar.maxValue = damageableObject.Health;
    }

    private void DamageableObject_OnDamageableDestroyed(object sender, IDamageable.TowerDestroyedEventArgs e)
    {
        damageableObject.OnDamageableDamaged -= DamageableObject_OnDamageableDamaged; ;
        damageableObject.OnDamageableDestroyed -= DamageableObject_OnDamageableDestroyed;

        healthbar.gameObject.SetActive(false);
    }

    private void DamageableObject_OnDamageableDamaged(object sender, System.EventArgs e)
    {
        healthbar.gameObject.SetActive(true);
        healthbar.value = damageableObject.Health;
    }
}