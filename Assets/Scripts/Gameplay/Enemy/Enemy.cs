using System;
using Gameplay.Enemy.States;
using UnityEngine;

namespace Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Scaler")] 
        [SerializeField] private EntityScaler entityScaler;
        
        [Header("Movement properties")]
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float speedMovement = 7f;
        [SerializeField] private float minSpeedMovement = 1.5f;
        
        [Header("Hunger properties")]
        [SerializeField] private float foodCheckRadius;
        [SerializeField] private LayerMask whatIsFood;

        private Rigidbody2D _rigidbody;
        
        public HungerState HungerState { get; private set; }
        
        public EnemyState CurrentState { get; private set; }
        
        public float CurrentSpeedMovement { get; private set; }

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            HungerState = new HungerState(_rigidbody, foodCheckRadius, whatIsFood);
            
            HungerState.Initialize(this);

            CurrentState = HungerState;
        }

        private void Start()
        {
            CurrentSpeedMovement = speedMovement;
            
            entityScaler.OnChangeScale += value =>
            {
                CurrentSpeedMovement = speedMovement * movementCurve.Evaluate(value / entityScaler.MaxValue);

                if (CurrentSpeedMovement < minSpeedMovement)
                {
                    CurrentSpeedMovement = minSpeedMovement;
                }
            };
        }

        private void Update()
        {
            CurrentState?.Update();
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}
