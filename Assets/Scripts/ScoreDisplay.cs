using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI ScoreText;

    private void OnEnable()
    {
        LevelManager.CurrentScore += UpdateScore;
    }

    private void OnDisable()
    {
        LevelManager.CurrentScore -= UpdateScore;
    }

    private void UpdateScore(int current, int required)
    {
        ScoreText.text = current.ToString() + "/" + required.ToString();
    }
}
