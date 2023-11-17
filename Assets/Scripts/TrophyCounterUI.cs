using System.Collections;
using TMPro;
using UnityEngine;

public class TrophyCounterUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI playerTrophyCounter;
    [SerializeField] private TextMeshProUGUI enemyTrophyCounter;

    private void Start()
    {
        GameplaySystem.Instance.OnTrophyCountChanged += GameplaySystem_OnTrophyCountChanged;
        playerTrophyCounter.text = "0";
        enemyTrophyCounter.text = "0";
    }

    private void GameplaySystem_OnTrophyCountChanged(object sender, System.EventArgs e)
    {
        GameplaySystem.Instance.GetScore(out int playerScore, out int enemyScore);
        playerTrophyCounter.text = playerScore.ToString();
        enemyTrophyCounter.text = enemyScore.ToString();
    }
}