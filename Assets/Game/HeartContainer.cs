using Blazers;
using Blazers.UI;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public static GameObject HeartPrefab = Resources.Load<GameObject>("UI/Heart");

    public List<Heart> Hearts = new();

    public LifeEntity Life;
    public GridLayoutGroup Grid;

    void Start()
    {
        Life.OnDamaged += UpdateHearts;
        CreateHearts();
    }

    void CreateHearts()
    {

    }
    public void UpdateHearts(LifeEntity life)
    {
        if (life.Health != Hearts.Count)
        {
            int difference = life.Health - Hearts.Count;

            if (difference > life.Health)
            {
                for (int i = 0; i < difference; i++)
                {
                    AddHeart();
                }
            }
            else
            {
                for (int i = 0; i < difference; i++)
                {
                    RemoveHeart();
                }
            }
        }
    }
    void AddHeart()
    {
        Heart heart = Instantiate(HeartPrefab).GetComponent<Heart>();
        Hearts.Add(heart);
        //Grid.
    }
    void RemoveHeart()
    {

    }
}
