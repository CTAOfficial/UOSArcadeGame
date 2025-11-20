using System;
using Glorp;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Action OnGameEnd;
    public static Action<int> OnScore;

    [SerializeField] int score;
    [SerializeField] GameObject GameOverUI;

    void Awake()
    {
        if (Instance) { throw new InvalidOperationException(); }
        Instance = this;

        LifeEntity.OnCreated += OnNewEntity;
        Player.OnDeath += EndGame;

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }


    void OnNewEntity(LifeEntity entity)
    {
        if (entity.IsPlayer) { return; }

        entity.OnDeath += HandleDeath;
    }

    private void HandleDeath(LifeEntity entity)
    {
        score += entity.ScoreValue; 
        OnScore?.Invoke(score);
    }

    void EndGame(Player player)
    {
        OnGameEnd?.Invoke();
        //Instantiate(GameOverUI);
        GameOverUI.SetActive(true);
    }
    
}
