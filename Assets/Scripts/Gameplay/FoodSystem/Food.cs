using System;
using UnityEngine;

namespace Gameplay.FoodSystem
{
    public class Food : MonoBehaviour
    {
        public event Action OnEat;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnEat?.Invoke();
            
            Destroy(gameObject);
        }
    }
}
