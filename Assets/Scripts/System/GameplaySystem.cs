using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public class GameplaySystem : MonoBehaviour
{
    public static GameplaySystem Instance { get; private set; }

    private SingleParticipantData playerData;
    private SingleParticipantData enemyData;

    public event EventHandler OnTrophyCountChanged;
    public event Action<GameState> OnGameStateChanged;

    // Timer
    [SerializeField][Tooltip("in seconds")] private float battleTime;
    [SerializeField] private float loadingTime = 5;
    private float countdownBattleTimer;
    private float countdownLoadingTimer;

    private string gameResult = "";

    private GameState gameState;

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
        countdownBattleTimer = battleTime;
        countdownLoadingTimer = loadingTime;

        playerData = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Player];
        enemyData = ParticipantDataManager.Instance.ParticipantDictionary[Participant.Enemy];

        TowerListing();

        SumTotalMaxHealth();
    }

    private void Update()
    {
        switch (gameState)
        {
            case GameState.Loading:
                LoadingState();
                break;
            case GameState.Battle:
                BattleState();
                break;
            case GameState.Result:
                ResultState();
                break;
        }
    }

    private void TowerListing()
    {
        Tower[] towerlist = FindObjectsOfType<Tower>();

        foreach (Tower tower in towerlist)
        {
            if (tower.Participant == Participant.Player)
            {
                playerData.TowerList.Add(tower);
            }
            else if (tower.Participant == Participant.Enemy)
            {
                enemyData.TowerList.Add(tower);
            }

            tower.OnTowerDestroyed += Tower_OnTowerDestroyed;
        }
    }

    private void Tower_OnTowerDestroyed(object sender, Tower.TowerDestroyedEventArgs e)
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

    private void LoadingState()
    {
        countdownLoadingTimer -= Time.deltaTime;
        if (countdownLoadingTimer <= 0)
        {
            UpdateGameState(GameState.Battle);
        }
    }

    private void BattleState()
    {
        countdownBattleTimer -= Time.deltaTime;
        if (countdownBattleTimer <= 0)
        {
            UpdateGameState(GameState.Result);
        }
    }

    private void ResultState()
    {
        gameResult = DetermineWinner();
    }

    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

        OnGameStateChanged?.Invoke(newState);
    }

    public string GetCurrentTimerConverted()
    {
        int minutes = (int)countdownBattleTimer / 60;
        int seconds = (int)countdownBattleTimer % 60;
        return $"{minutes:00}:{seconds:00}";
    }

    private string DetermineWinner()
    {
        if (gameResult != "")
        {
            return gameResult;
        }
        else
        {
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

                float playerDamage = CalculateDamagePercentage(playerData.TotalCurrentHealth, playerData.TotalMaxHealth);
                print("enemy damage to player is " + playerDamage);
                float enemyDamage = CalculateDamagePercentage(enemyData.TotalCurrentHealth, enemyData.TotalMaxHealth);
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
    }

    private void SumTotalCurrentHealth()
    {
        foreach (Tower tower in playerData.TowerList)
        {
            playerData.TotalCurrentHealth += tower.Health;
        }

        foreach (Tower tower in enemyData.TowerList)
        {
            enemyData.TotalCurrentHealth += tower.Health;
        }

        print("player total current health is " + playerData.TotalCurrentHealth);
        print("enemy total current health is " + enemyData.TotalCurrentHealth);
    }

    private void SumTotalMaxHealth()
    {
        foreach (Tower tower in playerData.TowerList)
        {
            playerData.TotalMaxHealth += tower.Health;
        }

        foreach (Tower tower in enemyData.TowerList)
        {
            enemyData.TotalMaxHealth += tower.Health;
        }
        print("player total max health is " + playerData.TotalMaxHealth);
        print("enemy total max health is " + enemyData.TotalMaxHealth);
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

    public string GetCountdownLoadingTimer()
    {
        return $"{(int)countdownLoadingTimer}";
    }

    public GameState GetCurrentState()
    {
        return gameState;
    }
}