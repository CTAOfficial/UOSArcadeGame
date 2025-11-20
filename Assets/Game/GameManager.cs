using System;
using Glorp;
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
    //[SerializeField] GameObject GameOverUI;

    void Awake()
    {
        if (Instance) { Destroy(gameObject); }
        Instance = this;

        LifeEntity.OnCreated += OnNewEntity;
        Player.OnDeath += EndGame;

        DontDestroyOnLoad(this);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    void OnDestroy()
    {
        LifeEntity.OnCreated -= OnNewEntity;
        Player.OnDeath -= EndGame;
    }


    void OnNewEntity(LifeEntity entity)
    {
        if (entity.IsPlayer) { return; }

        entity.OnDeath += HandleDeath;
    }

    private void HandleDeath(LifeEntity entity)
    {
        entity.OnDeath -= HandleDeath;

        Score += entity.ScoreValue;
        AudioSource.resource = entity.DeathSound;
        AudioSource.Play();
    }

    public void ResetLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    void EndGame(Player player)
    {
        Player.OnDeath -= EndGame;

        OnGameEnd?.Invoke();
        //Instantiate(GameOverUI);
    }
    
}
