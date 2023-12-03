using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timerDisplay;

    void Update()
    {
        timerDisplay.text = GameplaySystem.Instance.GetCurrentTimerConverted();
    }
}