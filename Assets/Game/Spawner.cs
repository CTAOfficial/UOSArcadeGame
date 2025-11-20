using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;


namespace Glorp
{
    [RequireComponent(typeof(Timer))]
    public sealed class Spawner : MonoBehaviour
    {
        public event Action<GameObject> OnSpawn;

        [Header("")]
        public Timer difficultyTimer;
        public float increaseTime;
        public float increaseIntervals;
        public float limit;

        [Header("Spawn Timer")]
        public Timer spawnTimer;
        [Range(0.1f, 30)] public float SpawnTime;

        [Header("Lists")]
        public List<GameObject> PrefabList = new();
        public List<Transform> SpawnList = new();
        List<GameObject> SpawnedObjects = new();


        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            GameManager.OnGameEnd += Shutdown;

            if (!spawnTimer) { spawnTimer = GetComponent<Timer>(); }

            spawnTimer.Resets = true;
            spawnTimer.OnTime += SpawnRandom;
            spawnTimer.Time = SpawnTime;

            if (difficultyTimer)
            {
                difficultyTimer.Resets = true;
                difficultyTimer.OnTime += IncreaseDifficulty;
                difficultyTimer.Time = increaseTime;
            }

            StartTimer();
        }

        void IncreaseDifficulty()
        {
            if (spawnTimer.Time <= limit) { difficultyTimer.ForceStop(); Debug.Log($"{difficultyTimer.name}: Limit reached, stopping."); }
            spawnTimer.Time -= increaseIntervals;
            if (spawnTimer.Time <= limit) { difficultyTimer.ForceStop(); Debug.Log($"{difficultyTimer.name}: Limit reached, stopping."); }
        }

        private void Shutdown()
        {
            spawnTimer.ForceStop();
            gameObject.SetActive(false);
        }

        GameObject GetRandomPrefab()
        {
            bool valid = false;
            GameObject select = null;

            while (!valid)
            {
                select = PrefabList[Random.Range(0, PrefabList.Count - 1)];
                if (select != null) { valid = true; }
            }

            return select;
        }
        Transform GetRandomSpawn()
        {
            bool valid = false;
            Transform select = null;

            while (!valid)
            {
                select = SpawnList[Random.Range(0, SpawnList.Count - 1)];
                if (select != null) { valid = true; }
            }

            return select;
        }

        void RemoveObject(GameObject gameObject)
        {
            SpawnedObjects.Remove(gameObject);
            if (gameObject.TryGetComponent(out LifeEntity lifeEntity))
            {
                lifeEntity.OnDeath -= (entity) => RemoveObject(entity.gameObject);
            }
        }

        void Spawn(GameObject prefab, Vector3 location)
        {
            GameObject spawned = Instantiate(prefab, location, Quaternion.identity);
            SpawnedObjects.Add(spawned);

            if (spawned.TryGetComponent(out LifeEntity lifeEntity))
            {
                lifeEntity.OnDeath += (entity) => RemoveObject(entity.gameObject);
            }

            OnSpawn?.Invoke(spawned);
        }
        void SpawnRandom()
        {
            Spawn(GetRandomPrefab(), GetRandomSpawn().position);
        }


        public void StartTimer()
        {
            if (PrefabList.Count > 0 && SpawnList.Count > 0) { StartCoroutine(spawnTimer.StartTimer()); }
            
            if (difficultyTimer) { StartCoroutine(difficultyTimer.StartTimer()); }
        }
    }

}