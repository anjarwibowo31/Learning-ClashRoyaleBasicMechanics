using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public enum GameState
{
    Loading,
    Battle,
    Result
}

public class GameplaySystem : MonoBehaviour
{
    public static GameplaySystem Instance { get; private set; }
    private ParticipantDataManager playerData;
    private ParticipantDataManager enemyData;

    public event EventHandler OnTrophyCountChanged;

    // Timer
    [SerializeField][Tooltip("in seconds")] private float battleTime;
    private float currentTime;
    private float playerTotalMaxHealth = 0;
    private float enemyTotalMaxHealth = 0;
    private float playerTotalCurrentHealth = 0;
    private float enemyTotalCurrentHealth = 0;
    private string gameResult = "";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        currentTime = battleTime;

        playerData = new ParticipantDataManager();
        enemyData = new ParticipantDataManager();

        TowerListing();
        SumTotalMaxHealth();
    }

    private void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
        }
        else
        {
            gameResult = DetermineWinner();
            print("Result is " + gameResult);
        }
        GetCurrentTimerConverted();
    }

    

    private void TowerListing()
    {
        Tower[] towerlist = FindObjectsOfType<Tower>();

        foreach (Tower tower in towerlist)
        {
            if (tower.CompareTag("PlayerTower"))
            {
                playerData.TowerList.Add(tower);
            }
            else if (tower.CompareTag("EnemyTower"))
            {
                enemyData.TowerList.Add(tower);
            }

            tower.OnTowerDestroyed += Tower_OnTowerDestroyed;
        }
    }

    private void Tower_OnTowerDestroyed(object sender, TowerDestroyedEventArgs e)
    {
        for (int i = 0; i <= playerData.TowerList.Count - 1; i++)
        {
            if (playerData.TowerList[i] == e.DestroyedTower)
            {
                playerData.TowerList.RemoveAt(i);
                ++enemyData.Score;
                break;
            }
        }

        for (int i = 0; i <= enemyData.TowerList.Count - 1; i++)
        {
            if (enemyData.TowerList[i] == e.DestroyedTower)
            {
                enemyData.TowerList.RemoveAt(i);
                ++playerData.Score;
                break;
            }
        }

        OnTrophyCountChanged?.Invoke(this, EventArgs.Empty);
    }

    public string GetCurrentTimerConverted()
    {
        int minutes = (int)currentTime / 60;
        int seconds = (int)currentTime % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    private string DetermineWinner()
    {
        if (gameResult != "") return gameResult;

        if (playerData.Score > enemyData.Score)
        {
            return "Player Win";
        }
        else if (enemyData.Score > playerData.Score)
        {
            return "Enemy Win";
        }
        else
        {
            SumTotalCurrentHealth();

            float playerDamage = CalculateDamagePercentage(playerTotalCurrentHealth, playerTotalMaxHealth);
            print("enemy damage to player is " + playerDamage);
            float enemyDamage = CalculateDamagePercentage(enemyTotalCurrentHealth, enemyTotalMaxHealth);
            print("player damage to enemy is " + enemyDamage);
            
            if (playerDamage > enemyDamage)
            {
                return "Enemy Win";
            }
            else if (enemyDamage > playerDamage)
            {
                return "Player Win";
            }
            else
            {
                return "Draw";
            }
        }
    }

    private void SumTotalCurrentHealth()
    {
        foreach (Tower tower in playerData.TowerList)
        {
            playerTotalCurrentHealth += tower.Health;
        }

        foreach (Tower tower in enemyData.TowerList)
        {
            enemyTotalCurrentHealth += tower.Health;
        }

        print("player total current health is " + playerTotalCurrentHealth);
        print("enemy total current health is " + enemyTotalCurrentHealth);
    }

    private void SumTotalMaxHealth()
    {
        foreach (Tower tower in playerData.TowerList)
        {
            playerTotalMaxHealth += tower.Health;
        }

        foreach (Tower tower in enemyData.TowerList)
        {
            enemyTotalMaxHealth += tower.Health;
        }
        print("player total max health is " + playerTotalMaxHealth);
        print("enemy total max health is " + enemyTotalMaxHealth);
    }

    public float CalculateDamagePercentage(float currentHealth, float MaxHealth)
    {
        return 100 - ((currentHealth / MaxHealth) * 100);
    }

    public void GetScore(out int playerScore, out int enemyScore)
    {
        playerScore = playerData.Score;
        enemyScore = enemyData.Score;
    }
}