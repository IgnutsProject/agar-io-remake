using System;
using UnityEngine;

namespace Gameplay.Enemy
{
    public abstract class EnemyState
    {
        public Enemy Enemy { get; private set; }
        public Transform Transform { get; private set; }
        
        public event Action OnEnterState;
        public event Action OnExitState;

        public virtual void Initialize(Enemy enemy)
        {
            Enemy = enemy;
            Transform = enemy.transform;
        }

        public virtual void EnterState()
        {
            OnEnterState?.Invoke();
        }

        public virtual void ExitState()
        {
            OnExitState?.Invoke();
        }

        public abstract void Update();
        public abstract void FixedUpdate();
    }
}
