using System;
using UnityEngine;

namespace Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Movement properties")]
        [SerializeField] private float speedMovement = 7;
        [SerializeField] private Transform directionPoint;

        [Header("Rotation properties")]
        [SerializeField] private float sensitivity = 70f;
        [SerializeField] private Transform center;

        private Rigidbody2D _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            InputHandler.Instance.OnMouseX += value =>
            {
                center.Rotate(0, 0, value * sensitivity * Time.deltaTime);
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
                speedMovement * Time.deltaTime);
            
            _rigidbody.MovePosition(newPosition);
        }
    }
}