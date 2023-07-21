using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Button _continueButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _toMenuButton;
    [SerializeField] private GameObject _gameMenu;

    private void Awake()
    {
        _continueButton.onClick.AddListener(ContinueGame);
        _restartButton.onClick.AddListener(RestartGame);
        _toMenuButton.onClick.AddListener(ToMenu);
    }

    private void ToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    private void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void ContinueGame()
    {
        Time.timeScale = 1;
        _gameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
