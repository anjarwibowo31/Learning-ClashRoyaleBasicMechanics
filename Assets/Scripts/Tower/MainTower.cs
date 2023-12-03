using UnityEngine;

public class MainTower : Tower
{
    [SerializeField] GameObject canon;
    protected override void Start()
    {
        base.Start();

        canon.SetActive(false);
        foreach (Tower tower in ParticipantDataManager.Instance.ParticipantDictionary[Participant].TowerList)
        {
            if (tower.TryGetComponent(out WingTower wingTower))
            {
                wingTower.OnWingTowerDestroyed += WingTower_OnWingTowerDestroyed;
            }
        }
    }

    private void WingTower_OnWingTowerDestroyed()
    {
        canon.SetActive(true);
    }

    public override void GetDamage(float damageAmount)
    {
        base.GetDamage(damageAmount);
    }

    public override void GetDestroyed()
    {
        base.GetDestroyed();

        GameplaySystem.Instance.UpdateGameStateForceLose(GameState.Result, Participant);
    }
}
