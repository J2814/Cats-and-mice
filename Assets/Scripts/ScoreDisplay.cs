using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

        transform.DOScale(new Vector3(1, 1, 1), 0);
        transform.DOPunchScale(new Vector3(0.5f, 0.5f, 0.5f), 0.15f);
    }
}
