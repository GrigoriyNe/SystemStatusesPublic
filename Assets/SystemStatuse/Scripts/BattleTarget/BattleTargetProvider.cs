using System;
using System.Collections.Generic;
using UnityEngine;

namespace StatusGroup
{
    public class BattleTargetProvider : MonoBehaviour, IBattleTarget
    {
        [SerializeField] private string _id;
        [SerializeField] private StatusManager _statusManager;
        [SerializeField] private BattleTargetType _type;
        [SerializeField] private StatusProperty _otherEffectForChangeProperty;
        [SerializeField] private InteractionInjection _interactionInjection;

        private List<string> _addedCompanions = new();

        public string Id => _id;
        public StatusManager StatusManager => _statusManager;
        public BattleTargetType Type => _type;
        public StatusProperty OtherEffectForChange => _otherEffectForChangeProperty;
        public InteractionInjection InteractionInjection => _interactionInjection;

        public event Action<BattleTargetProvider> OnSelected;

        private void OnEnable()
        {
            if (_interactionInjection == null)
            {
                _interactionInjection = _statusManager.InteractionInjection;
            }

            Registration();
        }

        private void OnMouseDown() =>
            OnSelected?.Invoke(this);

        public void Init(StatusManager statusManager) =>
            _statusManager = statusManager;

        public void AddCompanion(string companionId) =>
            _addedCompanions.Add(companionId);

        public bool HaveCompanion(string companionId)
        {
            foreach (string item in _addedCompanions)
            {
                if (item == companionId)
                {
                    return true;
                }    
            }
            return false;
        }

        public void OnUseAbility() =>
            _statusManager.UpdateAfteAbilityPlayer(this);

        public void OnDamageTaken() =>
            _statusManager.UpdateAfteDamageTakenPlayer(this);

        private void Registration()
        {
            switch (_type)
            {
                case BattleTargetType.Enemy:
                    _interactionInjection.SetEnemyUnit(this);
                    break;

                case BattleTargetType.Player:
                    _interactionInjection.SetFrendlyUnit(this);
                    break;

                case BattleTargetType.Companion:
                    _interactionInjection.SetFrendlyUnit(this);
                    break;

                case BattleTargetType.Other:
                    _interactionInjection.SetAnotherUnit(this);
                    break;
            }
        }
    }
}