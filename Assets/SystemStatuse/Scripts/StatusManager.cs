using System.Collections.Generic;
using UnityEngine;

namespace StatusGroup
{
    public class StatusManager : MonoBehaviour
    {
        [SerializeField] private InteractionInjection _interactionInjection;

        private Dictionary<string, Status> _activeStatuses = new Dictionary<string, Status>();
        private List<Status> _statusesToRemove = new List<Status>();

        public InteractionInjection InteractionInjection => _interactionInjection;

        public void Update()
        {
            _statusesToRemove.Clear();

            foreach (var status in _activeStatuses.Values)
            {
                status.UpdateDuration(Time.deltaTime);

                if (status.RemoveConditionType == RemoveConditionType.AfterDuration
                    && Time.deltaTime >= status.Duration)
                {
                    _statusesToRemove.Add(status);
                }
            }

            foreach (var status in _statusesToRemove)
            {
                RemoveStatus(status);
            }
        }

        public void UpdateStatusesOnTurnStart()
        {
            List<string> removedStatuses = new List<string>();

            foreach (Status status in _activeStatuses.Values)
            {
                if (status.DurationType == DurationType.Rounds)
                {
                    if (status.SubtractRound() == false)
                    {
                        removedStatuses.Add(status.Id + status.Target.Id);
                        status.FinishEffect(status.Target);
                    }
                    else
                    {
                        status.ApplyEffect(status.Target);
                    }
                }
            }

            RemoveStatus(removedStatuses);
        }

        public void UpdateAfteAttackPlayer(BattleTargetProvider target)
        {
            foreach (Status status in _activeStatuses.Values)
            {
                if (status.TriggerLogicType == TriggerLogicType.OnDamageDealt)
                {
                    status.ApplyEffect(target);
                }
            }
        }

        public void UpdateAfteAbilityPlayer(BattleTargetProvider target)
        {
            foreach (Status status in _activeStatuses.Values)
            {
                if (status.TriggerLogicType == TriggerLogicType.OnAbilityUse)
                {
                    status.ApplyEffect(status.Target);
                }
            }
        }

        public void UpdateAfteDamageTakenPlayer(BattleTargetProvider battleTargetProvider)
        {
            foreach (Status status in _activeStatuses.Values)
            {
                if (status.TriggerLogicType == TriggerLogicType.OnDamageTaken)
                {
                    status.ApplyEffect(status.Target);
                }
            }
        }

        public void UpdateStatusesOnTurnEnd()
        {
            List<string> removedStatuses = new List<string>();

            foreach (Status status in _activeStatuses.Values)
            {
                if (status.RemoveConditionType == RemoveConditionType.EndOfRound)
                {
                    removedStatuses.Add(status.Id + status.Target.Id);
                    status.FinishEffect(status.Target);
                }
            }

            RemoveStatus(removedStatuses);
        }

        public void RemoveStatus(List<string> statuses)
        {
            foreach (string item in statuses)
            {
                _activeStatuses.Remove(item);
            }
        }

        public bool AddStatus(Status newStatus)
        {
            if (newStatus.Target == null)
                return false;

            if (_activeStatuses.ContainsKey(newStatus.Id + newStatus.Target.Id))
            {
                Status existingStatus = _activeStatuses[newStatus.Id + newStatus.Target.Id];

                if (existingStatus.Type == StatusType.Unique)
                {
                    RemoveStatus(existingStatus);
                    _activeStatuses[newStatus.Id + newStatus.Target.Id] = newStatus;

                    return true;
                }
                else if (existingStatus.CanStack)
                {
                    existingStatus.AddStacks(newStatus.CurrentStacks);

                    Debug.Log($"Добавлен стак к {newStatus.Name}." +
                        $"Стаков: {existingStatus.CurrentStacks}");

                    return true;
                }

                Debug.Log($"Не может добавляться в стак");

                return false;
            }
            else
            {
                _activeStatuses[newStatus.Id + newStatus.Target.Id] = newStatus;

                foreach (StatusEffectBase effect in newStatus.Effects)
                {
                    if (newStatus.TriggerLogicType == TriggerLogicType.OnCardUse)
                    {
                        newStatus.ApplyEffect(newStatus.Target);
                    }
                    else
                    {
                        Debug.Log("Статус ожидает выполнения условий");
                    }
                }

                return true;
            }
        }

        public bool RemoveStatus(Status status)
        {
            if (_activeStatuses.ContainsKey(status.Id + status.Target.Id))
            {
                foreach (var effect in status.Effects)
                {
                    effect.Remove(status.Target);
                }
                _activeStatuses.Remove(status.Id + status.Target.Id);

                return true;
            }
            return false;
        }

        public bool MoveStatus(Status status, BattleTargetProvider newTarget)
        {
            if (status.CanBeMoved == false)
                return false;

            RemoveStatus(status);
            status.SetTarget(newTarget);

            return AddStatus(status);
        }

        public Status GetMovableStatus(BattleTargetProvider target)
        {
            foreach (Status item in _activeStatuses.Values)
            {
                if (item.CanBeMoved && item.Target == target)
                {
                    return item;
                }
            }

            return null;
        }

        public bool SpendStatus(string statusId, int stacks)
        {
            if (_activeStatuses.TryGetValue(statusId, out var status))
            {
                return status.SpendStacks(stacks);
            }

            return false;
        }

        public void AddStack(string statusId)
        {
            foreach (string item in _activeStatuses.Keys)
            {
                if (item == statusId)
                {
                    _activeStatuses[item].AddStacks(1);
                }
            }
        }

        public void TriggerEffects(TriggerLogicType triggerType)
        {
            foreach (var status in _activeStatuses.Values)
            {
                if (status.TriggerLogicType == triggerType ||
                    status.TriggerLogicType == TriggerLogicType.Always)
                {
                    foreach (var effect in status.Effects)
                    {
                        effect.OnTrigger(status.Target, triggerType);
                    }
                }
            }
        }

        public int GetCurrenStack(string keyId)
        {
            foreach (string item in _activeStatuses.Keys)
            {
                if (item == keyId)
                {
                    return _activeStatuses[item].CurrentStacks;
                }
            }
            return 0;
        }

        public int GetCurrenStack(StatusEffectBase effect, string targetId)
        {
            foreach (Status status in _activeStatuses.Values)
            {
                foreach (StatusEffectBase item in status.Effects)
                {
                    if (item == effect && status.Target.Id == targetId)
                    {
                        return status.CurrentStacks;
                    }
                }
            }
            return 0;
        }
    }
}