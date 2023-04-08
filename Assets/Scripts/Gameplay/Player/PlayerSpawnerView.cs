using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay.Player
{
    public class PlayerSpawnerView : MonoBehaviour
    {
        [SerializeField] private PlayerSpawner playerSpawner;
        [SerializeField] private Button respawnButton;

        private void Start()
        {
            respawnButton.onClick.AddListener(() =>
            {
                playerSpawner.Respawn();
            });
        }
    }
}