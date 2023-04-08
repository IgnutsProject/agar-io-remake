using System;
using System.Collections.Generic;
using Gameplay.Player;
using UnityEngine;

namespace Gameplay.ScoreSystem
{
    public class ScoreManagerView : MonoBehaviour
    {
        [Header("Model")]
        [SerializeField] private ScoreManager scoreManager;

        [Header("UI")] 
        [SerializeField] private LeaderBoardSlotView slotViewPrefab;
        [SerializeField] private Transform layout;

        private List<LeaderBoardSlotView> _spawnedSlotViews = new List<LeaderBoardSlotView>();

        private void Start()
        {
            scoreManager.OnDataChanged += data =>
            {
                CleanUp();
                Initialize(data);
            };
        }

        private void Initialize(List<LeaderData> leaderList)
        {
            foreach (var enemy in leaderList)
            {
                CreateSlotView(enemy);
            }
        }

        private void CreateSlotView(LeaderData leader)
        {
            LeaderBoardSlotView slotView = Instantiate(slotViewPrefab, layout);

            slotView.LeaderNameText.text = $"{_spawnedSlotViews.Count + 1}. {leader.Name}";
            if (leader.IsPlayer)
            {
                slotView.Highlight();
            }
            
            _spawnedSlotViews.Add(slotView);
        }

        private void CleanUp()
        {
            foreach (var slot in _spawnedSlotViews)
            {
                Destroy(slot.gameObject);
            }
            
            _spawnedSlotViews.Clear();
        }
    }
}
