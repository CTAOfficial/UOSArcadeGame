using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;


    void Awake()
    {
        GameManager.OnScore += (score) => StartCoroutine(UpdateScore(score));
    }
    void OnDestroy()
    {
        GameManager.OnScore -= (score) => StartCoroutine(UpdateScore(score));
    }

    IEnumerator UpdateScore(int score)
    {
        ScoreText.text = string.Format("{0:0000000}", score);

        ScoreText.color = Color.yellow;
        yield return new WaitForSeconds(0.15f);
        ScoreText.color = Color.white;

    }
}
