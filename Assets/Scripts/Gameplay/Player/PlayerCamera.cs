using System;
using UnityEngine;

namespace Gameplay.Player
{
    public class PlayerCamera : MonoBehaviour
    {
        [SerializeField] private Vector3 offset = new Vector3(0,0,-10);
        [SerializeField] private Transform targetPoint;

        private void Update()
        {
            transform.position = targetPoint.position + offset;
        }
    }
}
