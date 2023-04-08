using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public static EnemySpawner Instance { get; private set; }
        
        [Header("Enemies properties")]
        [SerializeField] private Enemy[] enemiesPrefabs;
        
        [Header("Spawn points")]
        [SerializeField] private Transform leftBottomPoint;
        [SerializeField] private Transform rightTopPoint;
        
        private List<Enemy> _spawnedEnemiesList = new List<Enemy>();

        private void Awake()
        {
            if (Instance)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
        }

        private void Start()
        {
            for (int i = 0; i < GameConfig.StartEnemiesCount; i++)
            {
                Spawn();
            }
        }

        public IEnumerator<Enemy> GetEnemies()
        {
            foreach (var enemy in _spawnedEnemiesList)
            {
                yield return enemy;
            }
        }

        private void Spawn()
        {
            Enemy enemyToSpawn = enemiesPrefabs[Random.Range(0, enemiesPrefabs.Length)];
            Vector3 spawnPosition = new Vector3(
                Random.Range(leftBottomPoint.position.x, rightTopPoint.position.x),
                Random.Range(leftBottomPoint.position.y, rightTopPoint.position.y));

            Enemy spawnedEnemy = Instantiate(enemyToSpawn, spawnPosition, Quaternion.identity);

            spawnedEnemy.HunterHandler.OnDie += () =>
            {
                _spawnedEnemiesList.Remove(spawnedEnemy);
                Spawn();
            };
            
            _spawnedEnemiesList.Add(spawnedEnemy);
        }
    }
}
