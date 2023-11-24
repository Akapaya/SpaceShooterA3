using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{
    public int score = 0;

    public static UnityEvent<int> OnScoreIncreased = new UnityEvent<int>();

    public delegate void IncreaseScoreEvent(int value);
    public static IncreaseScoreEvent IncreaseScoreHandler;

    public delegate int GetScoreEvent();
    public static GetScoreEvent GetScoreHandler;

    private void OnEnable()
    {
        IncreaseScoreHandler += IncreaseScore;
        GetScoreHandler += GetScore;
    }

    private void OnDisable()
    {
        IncreaseScoreHandler -= IncreaseScore;
        GetScoreHandler -= GetScore;
    }

    private void IncreaseScore(int value)
    {
        score += value;
        OnScoreIncreased.Invoke(score);
    }

    private int GetScore() 
    {
        return score;
    }
}