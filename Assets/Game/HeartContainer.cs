using Blazers;
using Blazers.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public static GameObject HeartPrefab;

    public List<Heart> Hearts = new();

    public LifeEntity Life;
    public GridLayoutGroup Grid;

    void Start()
    {
        if (!HeartPrefab) { HeartPrefab = Resources.Load<GameObject>("UI/Heart"); }

        Life.OnDamaged += UpdateHearts;
        CreateHearts();
    }

    void CreateHearts()
    {
        for (int i = 0; i < Life.Health; i++)
        {
            CreateHeart();
        }
        //UpdateHearts(Life);
    }
    public void UpdateHearts(LifeEntity life)
    {
        if (life.Health != Hearts.Count)
        {
            int difference = life.Health - Hearts.Count;
            HeartState state;

            if (difference > life.Health)
            {
                state = HeartState.Active;
            }
            else
            {
                state = HeartState.Lost;
            }

            for (int i = 0; difference > i; i++)
            {
                UpdateHeart(i, state);
            }
        }
    }
    void CreateHeart()
    {
        Heart heart = Instantiate(HeartPrefab, transform).GetComponent<Heart>();
        Hearts.Add(heart);
    }
    void UpdateHeart(int index, HeartState state)
    {
        Hearts[index].State = state;
    }
}
