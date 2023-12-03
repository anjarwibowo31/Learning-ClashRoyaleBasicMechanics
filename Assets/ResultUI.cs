using TMPro;
using UnityEngine;

public class ResultUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI condition;
    [SerializeField] private GameObject resultUIObject;

    private void Start()
    {
        GameplaySystem.Instance.OnGameStateChanged += GameplaySystem_OnGameStateChanged;

        resultUIObject.SetActive(false);
    }

    private void GameplaySystem_OnGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.Result)
        {
            resultUIObject.SetActive(true);
        }
        else
        {
            resultUIObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        condition.text = GameplaySystem.Instance.GameResult;
    }
}
