using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement properties")]
        [SerializeField] private float speedMovement = 7;
        [SerializeField] private Transform directionPoint;

        [Header("Rotation properties")]
        [SerializeField] private float sensitivity = 70f;
        [SerializeField] private Transform center;

        [Header("Components")] 
        [SerializeField] private EntityScaler entityScaler;

        private Rigidbody2D _rigidbody;
        private float _currentSpeedMovement;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            _currentSpeedMovement = speedMovement;
            
            InputHandler.Instance.OnMouseX += value =>
            {
                center.Rotate(0, 0, value * sensitivity * Time.deltaTime);
            };

            entityScaler.OnChangeScale += value =>
            {
                _currentSpeedMovement = speedMovement - speedMovement * value / 50;
            };
        }

        private void FixedUpdate()
        {
            MoveToDirection();
        }

        private void MoveToDirection()
        {
            Vector3 newPosition = Vector3.MoveTowards(
                _rigidbody.position,
                directionPoint.position,
                _currentSpeedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}