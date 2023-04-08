using Gameplay.FoodSystem;
using UnityEngine;

namespace Gameplay.Enemy.States
{
    public class HungerState : EnemyState
    {
        private Rigidbody2D _rigidbody;
        private float _foodCheckRadius;
        private LayerMask _whatIsFood;

        private Food _targetFood;

        public HungerState(Rigidbody2D rigidbody, float foodCheckRadius, LayerMask whatIsFood)
        {
            _rigidbody = rigidbody;
            _foodCheckRadius = foodCheckRadius;
            _whatIsFood = whatIsFood;
        }

        public override void Initialize(Enemy enemy)
        {
            base.Initialize(enemy);
            
            Enemy.OnPreyOutside += () =>
            {
                if (Enemy.CurrentState == this) return;
                
                Enemy.SetState(Enemy.HungerState);
            };
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            _targetFood = FindNearestFood();
            MoveToFood();
            
            Debug.DrawLine(Transform.position, _targetFood.Position);
        }

        private Food FindNearestFood()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Transform.position, _foodCheckRadius, _whatIsFood);

            Transform nearestFood = colliders[0].transform;
            float minDistance = Vector2.Distance(Transform.position, nearestFood.position);
            
            foreach (var col in colliders)
            {
                float distance = Vector2.Distance(Transform.position, col.transform.position);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    nearestFood = col.transform;
                }
            }

            return nearestFood.GetComponent<Food>();
        }

        private void MoveToFood()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position, 
                _targetFood.Position,
                Enemy.CurrentSpeedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}