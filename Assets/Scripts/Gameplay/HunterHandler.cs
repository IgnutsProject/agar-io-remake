using System;
using UnityEngine;

namespace Gameplay
{
    public class HunterHandler : MonoBehaviour
    {
        [Header("Graphic")] 
        [SerializeField] private SpriteRenderer spriteRenderer;

        [Header("Hunter properties")] 
        [SerializeField] private Transform centerPoint;
        [SerializeField] private float hunterCheckRadius = 0.01f;
        [SerializeField] private LayerMask whatIsHunter;

        [Header("Scale properties")]
        [SerializeField] private EntityScaler selfEntityScaler;
        [SerializeField] private float decreaseScaleFactor = 2;
        [SerializeField] private float targetScaleFactor = 0.1f;
        
        public event Action OnDie;

        private void Start()
        {
            OnDie += () =>
            {
                Destroy(transform.parent.gameObject);
            };
        }

        private void Update()
        {
            spriteRenderer.sortingOrder = (int) selfEntityScaler.Value;
        }

        private void FixedUpdate()
        {
            CheckHunter();
        }
        
        private void CheckHunter()
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(centerPoint.position, hunterCheckRadius, whatIsHunter);

            foreach (var col in colliders)
            {
                if (col.gameObject == gameObject || col.TryGetComponent<EntityScaler>(out var scaler) == false) continue;
                if (scaler.Value <= selfEntityScaler.Value + targetScaleFactor) continue;
                
                scaler.Value += selfEntityScaler.Value / decreaseScaleFactor;
                
                OnDie?.Invoke();
            }
        }
    }
}
