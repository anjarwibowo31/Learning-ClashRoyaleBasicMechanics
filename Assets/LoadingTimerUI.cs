using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timer;
    [SerializeField] private GameObject timerGameObject;

    private void Start()
    {
        GameplaySystem.Instance.OnGameStateChanged += GameplaySystem_OnGameStateChanged;
    }

    private void GameplaySystem_OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Loading)
        {
            timerGameObject.SetActive(true);
        }
        else
        {
            timerGameObject.SetActive(false);
        }
    }

    void Update()
    {
        timer.text = GameplaySystem.Instance.GetCountdownLoadingTimer();
    }
}
