using TMPro;
using UnityEngine;

namespace Gameplay.ScoreSystem
{
    public class LeaderBoardSlotView : MonoBehaviour
    {
        [SerializeField] private Color hightLighedColor = Color.yellow;
        [SerializeField] private TextMeshProUGUI leaderNameText;

        public TextMeshProUGUI LeaderNameText => leaderNameText;

        public void Highlight()
        {
            leaderNameText.color = hightLighedColor;
        }
    }
}
