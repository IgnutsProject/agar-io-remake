using System;
using UnityEngine;

namespace Gameplay
{
    public class EntityScaler : MonoBehaviour
    {
        [Header("General properties")]
        [SerializeField] private float baseValue = 1;
        [SerializeField] private float increaseSpeed = 2f;
        
        private float _value;

        public Vector3 TargetScale => new Vector2(_value, _value);
        
        public float Value
        {
            get => _value;
            set => _value = value;
        }

        private void Start()
        {
            Value = baseValue;
        }

        private void Update()
        {
            ChangeScale();
        }

        private void ChangeScale()
        {
            Vector3 newScale = Vector3.MoveTowards(
                transform.localScale,
                TargetScale,
                increaseSpeed * Time.deltaTime);
            
            transform.localScale = newScale;
        }
    }
}