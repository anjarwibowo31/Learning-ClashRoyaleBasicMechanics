using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ControlButtonUI : MonoBehaviour
{
    [SerializeField] private Button exitButton;
    [SerializeField] private Button restartButton;

    void Start()
    {
        exitButton.onClick.AddListener(ExitGame);

        restartButton.onClick.AddListener(RestartGame);
    }

    private void ExitGame()
    {
        print("Quit");
        Application.Quit();
    }

    private void RestartGame()
    {
        print("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
