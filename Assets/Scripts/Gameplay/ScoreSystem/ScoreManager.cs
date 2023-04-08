using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Enemy;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.ScoreSystem
{
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private float updateRate = 0.5f;
        
        private List<Enemy.Enemy> _sortedEnemies = new List<Enemy.Enemy>();

        public event Action<List<Enemy.Enemy>> OnDataChanged; 

        private void Start()
        {
            Initialize();

            StartCoroutine(UpdateDataDelay());
        }

        private void Initialize()
        {
            _sortedEnemies = GetSortedList();
            
            OnDataChanged?.Invoke(_sortedEnemies);
        }

        private List<Enemy.Enemy> GetSortedList()
        {
            IEnumerator<Enemy.Enemy> enemies = EnemySpawner.Instance.GetEnemies();
            List<Enemy.Enemy> enemiesList = new List<Enemy.Enemy>();
            
            while (enemies.MoveNext())
            {
                enemiesList.Add(enemies.Current);
            }

            for (int j = 0; j < enemiesList.Count; j++)
            {
                for (int i = 1; i < enemiesList.Count; i++)
                {
                    if (enemiesList[i].EntityScaler.Value > enemiesList[i - 1].EntityScaler.Value)
                    {
                        var temp = enemiesList[i];
                        enemiesList[i] = enemiesList[i - 1];
                        enemiesList[i - 1] = temp;
                    }
                }
            }
            
            return enemiesList;
        }

        private IEnumerator UpdateDataDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateRate);
                
                _sortedEnemies = GetSortedList();
            
                OnDataChanged?.Invoke(_sortedEnemies);
            }
        }
    }
}
