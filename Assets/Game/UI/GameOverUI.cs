using TMPro;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

namespace Glorp.UI
{
    public class GameOverUI : MonoBehaviour
    {
        public bool StartHidden { get; set; }
        [SerializeField] TextMeshProUGUI ScoreText;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            if (StartHidden) { Display(false); }
        }

        public void Display(bool state)
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

#if UNITY_EDITOR
    [CustomEditor(typeof(GameOverUI))]
    public class GameOverUIEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var script = (GameOverUI)target;

            if (GUILayout.Button("Show"))
            {
                script.Display(true);
            }
            if (GUILayout.Button("Hide"))
            {
                script.Display(false);
            }
        }
    }
#endif

}