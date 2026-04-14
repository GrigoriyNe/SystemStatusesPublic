using UnityEngine;
using UnityEngine.UI;

namespace StatusGroup
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private Button _endRound;
        [SerializeField] private StatusManager _statusManager;

        private void Awake() =>
            _endRound.onClick.AddListener(OnClickEnd);

        private void OnClickEnd()
        {
            _statusManager.UpdateStatusesOnTurnEnd();
            _statusManager.UpdateStatusesOnTurnStart();
        }
    }
}