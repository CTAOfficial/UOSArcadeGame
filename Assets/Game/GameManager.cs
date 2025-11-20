using System;
using System.Collections.Generic;
using Glorp;
using Glorp.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Action OnGameEnd;
    public static Action<int> OnScore;

    public AudioSource AudioSource;

    public static int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScore?.Invoke(Score);
        }
    }
    [SerializeField] static int _score;
    [SerializeField] GameObject GameOverUI;
    List<LifeEntity> subscribedEvents = new();

    void Awake()
    {
        if (Instance) { Destroy(gameObject); }
        Instance = this;

        DontDestroyOnLoad(gameObject);

        GameOverUI = Resources.Load<GameObject>("UI/Game Over UI");

        LifeEntity.OnCreated += OnNewEntity;
        Player.OnDeath += EndGame;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void OnDestroy()
    {
        //LifeEntity.OnCreated -= OnNewEntity;
        //Player.OnDeath -= EndGame;
        for (int i = 0; subscribedEvents.Count > 0; i++)
        {
            subscribedEvents[i].OnDeath -= HandleDeath;
            subscribedEvents.RemoveAt(i);
            i--;
        }
    }


    void OnNewEntity(LifeEntity entity)
    {
        entity.OnDeath += HandleDeath;
        subscribedEvents.Add(entity);
    }

    private void HandleDeath(LifeEntity entity)
    {
        entity.OnDeath -= HandleDeath;
        subscribedEvents.Remove(entity);

        AudioSource.resource = entity.DeathSound;
        AudioSource.Play();

        if (!entity.IsPlayer)
        {
            Score += entity.ScoreValue;
        }
    }

    public void ResetLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void EndGame(Player player)
    {
        OnGameEnd?.Invoke();
        Instantiate(GameOverUI).GetComponent<GameOverUI>().Display(true);
    }
    
}
