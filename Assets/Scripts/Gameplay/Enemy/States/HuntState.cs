using UnityEngine;

namespace Gameplay.Enemy.States
{
    public class HuntState : EnemyState
    {
        private Rigidbody2D _rigidbody;
        private float _preyCheckRadius;
        private LayerMask _whatIsPrey;

        private EntityScaler _preyEntityScaler;

        public HuntState(Rigidbody2D rigidbody, float preyCheckRadius, LayerMask whatIsPrey)
        {
            _rigidbody = rigidbody;
            _preyCheckRadius = preyCheckRadius;
            _whatIsPrey = whatIsPrey;
        }

        public override void Initialize(Enemy enemy)
        {
            base.Initialize(enemy);
            
            Enemy.OnPreyInside += () =>
            {
                if (Enemy.CurrentState == this) return;
                
                Enemy.SetState(Enemy.HuntState);
            };
        }

        public override void Update()
        {
            
        }

        public override void FixedUpdate()
        {
            _preyEntityScaler = FindNearestPrey();
            
            MoveToPrey();
            
            Debug.DrawLine(Transform.position, _preyEntityScaler.transform.position, Color.red);
        }

        private EntityScaler FindNearestPrey()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(Transform.position, _preyCheckRadius, _whatIsPrey);

            Transform preyTransform = Transform;
            float minDistance = float.MaxValue;
            
            foreach (var collider in colliders)
            {
                if (collider.gameObject == Enemy.gameObject) continue;

                if (preyTransform == Transform)
                {
                    preyTransform = collider.transform;
                    minDistance = Vector2.Distance(Transform.position, preyTransform.position);
                    continue;
                }
                
                float distance = Vector2.Distance(Transform.position, collider.transform.position);

                if (collider.transform.TryGetComponent<EntityScaler>(out var scaler) == false) continue;
                if (scaler.Value + GameConfig.TargetScaleFactor >= Enemy.EntityScaler.Value) continue;
                
                if (distance < minDistance)
                {
                    preyTransform = collider.transform;
                    minDistance = distance;
                }
            }

            return preyTransform.GetComponent<EntityScaler>();
        }

        private void MoveToPrey()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                _preyEntityScaler.transform.position,
                Enemy.CurrentSpeedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}