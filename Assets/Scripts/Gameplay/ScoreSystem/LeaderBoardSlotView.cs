using TMPro;
using UnityEngine;

namespace Gameplay.ScoreSystem
{
    public class LeaderBoardSlotView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI leaderNameText;

        public TextMeshProUGUI LeaderNameText => leaderNameText;
    }
}
