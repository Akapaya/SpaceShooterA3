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

    private void OnEnable()
    {
        IncreaseScoreHandler += IncreaseScore;
    }

    private void OnDisable()
    {
        IncreaseScoreHandler -= IncreaseScore;
    }

    private void IncreaseScore(int value)
    {
        score += value;
        OnScoreIncreased.Invoke(score);
    }
}