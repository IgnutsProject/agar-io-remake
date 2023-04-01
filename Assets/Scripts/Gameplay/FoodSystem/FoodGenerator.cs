using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.FoodSystem
{
    public class FoodGenerator : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Food[] foodsPrefabs;
        
        [Header("Borders points")]
        [SerializeField] private Transform leftBottomPoint;
        [SerializeField] private Transform rightTopPoint;
        
        private readonly List<Food> _spawnedFoodList = new List<Food>();

        private void Start()
        {
            Generate();
        }

        private void Generate()
        {
            for (int i = 0; i < GameConfig.StartFoodCount; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            Food food = foodsPrefabs[Random.Range(0, foodsPrefabs.Length)];
            Vector3 spawnPosition = new Vector3(
                Random.Range(leftBottomPoint.position.x, rightTopPoint.position.x),
                Random.Range(leftBottomPoint.position.y, rightTopPoint.position.y));
            
            Food spawnedFood = Instantiate(food, spawnPosition, Quaternion.identity);
            spawnedFood.OnEat += () =>
            {
                _spawnedFoodList.Remove(food);
                Spawn();
            };
            
            _spawnedFoodList.Add(spawnedFood);
        }
    }
}
