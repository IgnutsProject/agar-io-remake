using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [Header("Size properties")] 
        [SerializeField] private EntityScaler playerScaler;
        [SerializeField] private AnimationCurve sizeCurve;
        [SerializeField] private float maxSize = 25f;
        [SerializeField] private float changeSizeSpeed = 4f;
        
        [Header("Movement properties")]
        [SerializeField] private Vector3 offset = new Vector3(0,0,-10);
        [SerializeField] private Transform targetPoint;

        private Camera _camera;
        private float _defaultSize;
        private float _targetSize;
        
        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _defaultSize = _camera.orthographicSize;
        }

        private void Start()
        {
            playerScaler.OnChangeScale += value =>
            {
                _targetSize = _defaultSize + maxSize * sizeCurve.Evaluate(value / playerScaler.MaxValue);
            };
        }

        private void Update()
        {
            transform.position = targetPoint.position + offset;
            
            ChangeSizeToTarget();
        }

        private void ChangeSizeToTarget()
        {
            float newSize = Mathf.MoveTowards(
                _camera.orthographicSize,
                _targetSize, 
                changeSizeSpeed * Time.deltaTime);

            _camera.orthographicSize = newSize;
        }
    }
}
