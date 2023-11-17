using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarUI : MonoBehaviour
{
    private Slider healthbar;

    private Tower tower;

    private void Start()
    {
        tower = GetComponentInParent<Tower>();
        healthbar = GetComponentInChildren<Slider>();

        tower.OnTowerDamaged += Tower_OnTowerDamaged;
        tower.OnTowerDestroyed += Tower_OnTowerDestroyed;

        healthbar.gameObject.SetActive(false);
        healthbar.maxValue = tower.Health;
    }

    private void Tower_OnTowerDamaged(object sender, System.EventArgs e)
    {
        healthbar.gameObject.SetActive(true);
        healthbar.value = tower.Health;
    }

    private void Tower_OnTowerDestroyed(object sender, TowerDestroyedEventArgs e)
    {
        print("Healthbar should deactive");
        healthbar.gameObject.SetActive(false);
    }
}