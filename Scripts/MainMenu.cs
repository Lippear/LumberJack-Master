using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _exitButton;


    private void Awake()
    {
        _playButton.onClick.AddListener(LoadGameScene);
        _exitButton.onClick.AddListener(Exit);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    private void Exit()
    {
        Application.Quit();
    }
}
