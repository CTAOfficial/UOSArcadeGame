using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ScoreText;


    void Awake()
    {
        GameManager.OnScore += (score) => ScoreText.text = string.Format("{0:0000000}", score);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
