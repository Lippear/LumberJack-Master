using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _gameMenu;
    [SerializeField] private GameObject _completeMenu;
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private Button _pauseButton;
    [SerializeField] private Button _completeButton;

    private void Awake()
    {
        NpcBuilder.OnGetLogs += MakeCompleteLevelMenu;
        _completeButton.onClick.AddListener(CompleteLevel);
        _pauseButton.onClick.AddListener(SetPause);
    }

    private void OnDisable()
    {
        NpcBuilder.OnGetLogs -= MakeCompleteLevelMenu;
    }

    private void MakeCompleteLevelMenu()
    {
        _gameMenu.SetActive(false);
        _completeMenu.SetActive(true);
    }

    private void CompleteLevel()
    {
        SceneManager.LoadScene(0);
    }

    private void SetPause()
    {
        Time.timeScale = 0;
        _gameMenu?.SetActive(false);
        _pauseMenu.SetActive(true);
    }
}
