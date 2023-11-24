using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameoverPanel;
    [SerializeField] private TMP_Text scoreText;

    public delegate void GameOverEvent();
    public static GameOverEvent GameOverHandler;

    private void OnEnable()
    {
        GameOverHandler += GameOverPanel;
    }

    private void OnDisable()
    {
        GameOverHandler -= GameOverPanel;
    }

    public void GameOverPanel()
    {
        gameoverPanel.SetActive(true);
        scoreText.text = ScoreManager.GetScoreHandler?.Invoke().ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        SceneManager.LoadScene(0);
    }
}