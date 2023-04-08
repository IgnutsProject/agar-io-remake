using System;
using Gameplay.Enemy.States;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        [Header("Graphic")] 
        [SerializeField] private Color[] enemyColors;
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        [Header("Scaler")] 
        [SerializeField] private EntityScaler entityScaler;
        [SerializeField] private HunterHandler hunterHandler;
        
        [Header("Movement properties")]
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float speedMovement = 7f;
        [SerializeField] private float minSpeedMovement = 1.5f;
        
        [Header("Hunger properties")]
        [SerializeField] private float foodCheckRadius;
        [SerializeField] private LayerMask whatIsFood;

        [Header("Hunt state")] 
        [SerializeField] private float preyCheckRadius;
        [SerializeField] private LayerMask whatIsPrey;

        private Rigidbody2D _rigidbody;
        
        public HungerState HungerState { get; private set; }
        public HuntState HuntState { get; private set; }
        
        public EnemyState CurrentState { get; private set; }
        
        public float CurrentSpeedMovement { get; private set; }
        public string EnemyName { get; private set; }
        public EntityScaler EntityScaler => entityScaler;
        public HunterHandler HunterHandler => hunterHandler;

        public event Action OnPreyInside;
        public event Action OnPreyOutside;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            
            HungerState = new HungerState(_rigidbody, foodCheckRadius, whatIsFood);
            HuntState = new HuntState(_rigidbody, preyCheckRadius, whatIsPrey);
            
            HungerState.Initialize(this);
            HuntState.Initialize(this);

            CurrentState = HungerState;

            EnemyName = GameConfig.BotsNames[Random.Range(0, GameConfig.BotsNames.Length)];
        }

        private void Start()
        {
            CurrentSpeedMovement = speedMovement;
            spriteRenderer.color = enemyColors[Random.Range(0, enemyColors.Length)];
            
            entityScaler.OnChangeScale += value =>
            {
                CurrentSpeedMovement = speedMovement * movementCurve.Evaluate(value / entityScaler.MaxValue);

                if (CurrentSpeedMovement < minSpeedMovement)
                {
                    CurrentSpeedMovement = minSpeedMovement;
                }
            };

            hunterHandler.OnDie += () =>
            {
                Destroy(gameObject);
            };
        }

        private void Update()
        {
            CurrentState?.Update();
            
        }

        private void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
            
            if (CheckPrey())
            {
                OnPreyInside?.Invoke();
                return;
            }
            OnPreyOutside?.Invoke();
        }

        public void SetState(EnemyState enemyState)
        {
            CurrentState?.ExitState();
            CurrentState = enemyState;
            CurrentState?.EnterState();
        }

        private bool CheckPrey()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, preyCheckRadius, whatIsPrey);

            foreach (var col in colliders)
            {
                if (col.gameObject == gameObject) continue;
                if (col.TryGetComponent<EntityScaler>(out var scaler) == false) continue;
                if (scaler.Value + GameConfig.TargetScaleFactor >= entityScaler.Value) continue;


                return true;
            }
            
            return false;
        }
    }
}
