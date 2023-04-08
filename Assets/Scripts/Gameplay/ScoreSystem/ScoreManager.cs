using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Enemy;
using UnityEditor.Compilation;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Gameplay.ScoreSystem
{
    [Serializable]
    public class LeaderData
    {
        public string Name { get; }
        public float Value { get; }
        public bool IsPlayer { get; }

        public LeaderData(string name, float value)
        {
            Name = name;
            Value = value;
        }
        
        public LeaderData(string name, float value, bool isPlayer)
        {
            Name = name;
            Value = value;
            IsPlayer = isPlayer;
        }
    }
    
    public class ScoreManager : MonoBehaviour
    {
        [SerializeField] private EntityScaler playerEntityScaler;
        [SerializeField] private float updateRate = 0.5f;
        
        private List<LeaderData> _sortedLeaders = new List<LeaderData>();

        public event Action<List<LeaderData>> OnDataChanged; 

        private void Start()
        {
            Initialize();

            StartCoroutine(UpdateDataDelay());
        }

        private void Initialize()
        {
            _sortedLeaders = GetSortedList();
            
            OnDataChanged?.Invoke(_sortedLeaders);
        }

        private List<LeaderData> GetSortedList()
        {
            IEnumerator<Enemy.Enemy> enemies = EnemySpawner.Instance.GetEnemies();
            List<Enemy.Enemy> enemiesList = new List<Enemy.Enemy>();
            
            while (enemies.MoveNext())
            {
                enemiesList.Add(enemies.Current);
            }
            
            List<LeaderData> leaderDataList = new List<LeaderData>();

            leaderDataList.Add(new LeaderData("Player", playerEntityScaler.Value, true));
            foreach (var enemy in enemiesList)
            {
                leaderDataList.Add(new LeaderData(enemy.EnemyName, enemy.EntityScaler.Value));
            }

            for (int j = 0; j < leaderDataList.Count; j++)
            {
                for (int i = 1; i < leaderDataList.Count; i++)
                {
                    if (leaderDataList[i].Value > leaderDataList[i - 1].Value)
                    {
                        var temp = leaderDataList[i];
                        leaderDataList[i] = leaderDataList[i - 1];
                        leaderDataList[i - 1] = temp;
                    }
                }
            }
            
            return leaderDataList;
        }

        private IEnumerator UpdateDataDelay()
        {
            while (true)
            {
                yield return new WaitForSeconds(updateRate);
                
                _sortedLeaders = GetSortedList();
            
                OnDataChanged?.Invoke(_sortedLeaders);
            }
        }
    }
}
