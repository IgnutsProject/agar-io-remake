using System;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Gameplay.Player
{
    public class PlayerSpawner : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private HunterHandler playerHunterHandler;
        [SerializeField] private Transform bottomLeftPoint;
        [SerializeField] private Transform topRightPoint;

        [Header("Events")] 
        [SerializeField] private UnityEvent onPlayerDie;
        [SerializeField] private UnityEvent onPlayerRespawn;
        
        public event UnityAction OnPlayerDie
        {
            add => onPlayerDie.AddListener(value);
            remove => onPlayerDie.RemoveListener(value);
        }
        
        public event UnityAction OnPlayerRespawn
        {
            add => onPlayerRespawn.AddListener(value);
            remove => onPlayerRespawn.RemoveListener(value);
        }
        
        private void Start()
        {
            playerHunterHandler.OnDie += () =>
            {
                onPlayerDie?.Invoke();
            };
        }

        public void Respawn()
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(bottomLeftPoint.position.x, topRightPoint.position.x),
                Random.Range(bottomLeftPoint.position.y, topRightPoint.position.y));

            playerHunterHandler.transform.position = randomPosition;
            
            onPlayerRespawn?.Invoke();
        }
    }
}
