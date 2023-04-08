using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement properties")] 
        [SerializeField] private AnimationCurve movementCurve;
        [SerializeField] private float speedMovement = 7;
        [SerializeField] private float minSpeedMovement = 1.5f;

        [Header("Components")] 
        [SerializeField] private EntityScaler entityScaler;
        [SerializeField] private Camera playerCamera;

        private Rigidbody2D _rigidbody;
        private float _currentSpeedMovement;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _currentSpeedMovement = speedMovement;

            entityScaler.OnChangeScale += value =>
            {
                _currentSpeedMovement = speedMovement * movementCurve.Evaluate(value / entityScaler.MaxValue);

                if (_currentSpeedMovement < minSpeedMovement)
                {
                    _currentSpeedMovement = minSpeedMovement;
                }
            };
        }

        private void FixedUpdate()
        {
            MoveToDirection();
        }

        private Vector3 GetMovementDirection()
        {
            var hit = Physics2D.Raycast(playerCamera.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

            return hit.point;
        }

        private void MoveToDirection()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                GetMovementDirection(),
                _currentSpeedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}