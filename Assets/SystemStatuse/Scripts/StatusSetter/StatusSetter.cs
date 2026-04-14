using System;
using UnityEngine;
using UnityEngine.UI;

namespace StatusGroup
{
    [RequireComponent(typeof(StatusSetterView))]
    public class StatusSetter : MonoBehaviour
    {
        [SerializeField] private StatusManager _statusManager;
        [SerializeField] private StatusProperty _statusProperty;
        [SerializeField] private Button _activateButton;

        private StatusSetterView _cardView;
        public StatusProperty StatusProperty => _statusProperty;

        public event Action<StatusSetter> StatusSetted;

        private void Awake()
        {
            _activateButton.onClick.AddListener(OnSelected);
            _cardView = GetComponent<StatusSetterView>();
            _cardView.Init(_statusProperty.Name, _statusProperty.EffectDescription);
        }

        public void Activate(BattleTargetProvider target)
        {
            Status status = new Status
            (
                _statusProperty.Id,
                _statusProperty.Name,
                _statusProperty.Type,
                target,
                _statusProperty.TargetType,
                _statusProperty.CurrentStacks,
                _statusProperty.MaxStacks,
                _statusProperty.EffectDescription,
                _statusProperty.CanStack,
                _statusProperty.CanBeSpent,
                _statusProperty.CanBeMoved,
                _statusProperty.DurationType,
                _statusProperty.RemoveConditionType,
                _statusProperty.TriggerLogicType,
                _statusProperty.Duration,
                _statusProperty.TurnsRemaining,
                _statusProperty.Effects
            );

            _statusManager.AddStatus(status);
        }

        private void OnSelected() =>
            StatusSetted?.Invoke(this);
    }
}