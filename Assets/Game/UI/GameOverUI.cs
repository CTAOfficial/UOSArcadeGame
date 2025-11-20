using TMPro;
using UnityEngine;

namespace Glorp.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ScoreText;

        void Awake()
        {
            GameManager.OnGameEnd += () => Display(true);
        }
        void OnDestroy()
        {
            GameManager.OnGameEnd -= () => Display(true);
        }

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Display(false);
        }

        void Display(bool state)
        {
            for (int i = 0; transform.childCount > i; i++)
            {
                transform.GetChild(i).gameObject.SetActive(state);
            }

            switch (state)
            {
                case true:
                    
                    ScoreText.text = string.Format("{0:000000}", GameManager.Score);
                    break;

                case false:

                    break;

            }
        }


    }

}