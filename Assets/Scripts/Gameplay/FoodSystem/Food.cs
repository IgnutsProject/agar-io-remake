using System;
using UnityEngine;

namespace Gameplay.FoodSystem
{
    public class Food : MonoBehaviour
    {
        public Vector3 Position => transform.position;
        
        public event Action OnEat;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent<EntityScaler>(out var scaler) == false) return;

            scaler.Value += GameConfig.IncreaseScaleFactor;
            
            OnEat?.Invoke();
            
            Destroy(gameObject);
        }
    }
}
