using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] TMP_Text tmp_Score;

    private void OnEnable()
    {
        ScoreManager.OnScoreIncreased.AddListener(SetScoreText);
    }

    private void OnDisable()
    {
        ScoreManager.OnScoreIncreased.RemoveListener(SetScoreText);
    }

    private void SetScoreText(int score)
    {
        tmp_Score.text = "Score: " + score.ToString("0000");
    }
}