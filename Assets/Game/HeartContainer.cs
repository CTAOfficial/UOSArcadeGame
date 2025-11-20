using System.Collections.Generic;
using Glorp.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


namespace Glorp
{
    public class HeartContainer : MonoBehaviour
    {
        public static GameObject HeartPrefab;

        public List<Heart> Hearts = new();
        List<Heart> ActiveHearts = new();
        Queue<Heart> LostHearts = new();

        public LifeEntity Life;
        public GridLayoutGroup Grid;

        int lostIndex;


        void Start()
        {
            if (!HeartPrefab) { HeartPrefab = Resources.Load<GameObject>("UI/Heart"); }

            Life.OnDamaged += UpdateHearts;
            CreateHearts();
        }
        void OnDestroy()
        {
            Life.OnDamaged -= UpdateHearts;
        }

        void CreateHearts()
        {
            if (Life.Health > Hearts.Count)
            {
                int diff = Life.Health - Hearts.Count;

                for (int i = 0; i < diff; i++) { CreateHeart(); }
            }
        }
        public void UpdateHearts(LifeEntity life)
        {
            // CreateHearts();

            if (life.Health != Hearts.Count)
            {
                int diff = life.MaxHealth - life.Health;

                for (int i = 0; i < diff; i++)
                {
                    UpdateHeart(i, HeartState.Lost);
                }
            }
        }
        void UpdateHeart(int index, HeartState state)
        {
            Hearts[index].State = state;
        }

        void CreateHeart()
        {
            Heart heart = Instantiate(HeartPrefab, transform).GetComponent<Heart>();
            heart.State = HeartState.Active;

            Hearts.Add(heart);
            //ActiveHearts.Enqueue(heart);
        }
        /*void UpdateHeart(HeartState state)
        {
            Hearts[index].State = state;

            switch (state)
            {
                case HeartState.Active:
                    if (LostHearts.TryDequeue(out Heart heart))
                    {
                        ActiveHearts.Enqueue(heart);
                    }
                    break;
                case HeartState.Lost:
                    ActiveHearts.Dequeue();
                    LostHearts.Enqueue(heart);
                    break;
            }
        }*/
    
    }

}