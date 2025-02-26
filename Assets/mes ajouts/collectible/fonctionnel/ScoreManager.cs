using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TMP_Text scoreText;
    private int score = 0;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString();
    }

    public void MinusScore(int scoreToRemove)
    {
        score -= scoreToRemove;
        scoreText.text = score.ToString();
    }
}