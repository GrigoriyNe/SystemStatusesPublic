using System.Collections.Generic;
using UnityEngine;

namespace StatusGroup
{
    [System.Serializable]
    public class Status
    {
        public Status(string id,
            string name,
            StatusType type,
            BattleTargetProvider target,
            BattleTargetType targetType,
            int currentStacks,
            int maxStacks,
            string effectDescription,
            bool canStack,
            bool canBeSpent,
            bool canBeMoved,
            DurationType durationType,
            RemoveConditionType removeConditionType,
            TriggerLogicType triggerLogicType,
            float duration,
            int turnsRemaining, List<StatusEffectBase> effects)
        {
            Id = id;
            Name = name;
            Type = type;
            Target = target;
            TargetType = targetType;
            CurrentStacks = currentStacks;
            MaxStacks = maxStacks;
            EffectDescription = effectDescription;
            CanStack = canStack;
            CanBeSpent = canBeSpent;
            CanBeMoved = canBeMoved;
            DurationType = durationType;
            RemoveConditionType = removeConditionType;
            TriggerLogicType = triggerLogicType;
            Duration = duration;
            TurnsRemaining = turnsRemaining;
            Effects = effects;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public StatusType Type { get; private set; }
        public BattleTargetProvider Target { get; private set; }
        public BattleTargetType TargetType { get; private set; }
        public int CurrentStacks { get; private set; }
        public int MaxStacks { get; private set; }
        public string EffectDescription { get; private set; }
        public bool CanStack { get; private set; }
        public bool CanBeSpent { get; private set; }
        public bool CanBeMoved { get; private set; }
        public DurationType DurationType { get; private set; }
        public RemoveConditionType RemoveConditionType { get; private set; }
        public TriggerLogicType TriggerLogicType { get; private set; }

        public float Duration { get; private set; }
        public int TurnsRemaining { get; private set; }
        public List<StatusEffectBase> Effects { get; private set; } = new List<StatusEffectBase>();

        public void SetTarget(BattleTargetProvider target) =>
            Target = target;

        public void AddStacks(int stacks)
        {
            if (!CanStack) 
                return;

            CurrentStacks = Mathf.Min(CurrentStacks + stacks, MaxStacks);
        }

        public bool SpendStacks(int stacks)
        {
            if (!CanBeSpent || CurrentStacks < stacks)
            {
                return false;
            }

            CurrentStacks -= stacks;

            return true;
        }

        public bool SubtractRound()
        {
            if (TurnsRemaining > 0)
            {
                TurnsRemaining--;

                return true;
            }

            return false;
        }

        public void ApplyEffect(BattleTargetProvider target)
        {
            foreach (StatusEffectBase item in Effects)
            {
                item.Apply(target);
            }
        }

        public void FinishEffect(BattleTargetProvider target)
        {
            foreach (StatusEffectBase item in Effects)
            {
                item.Remove(target);
            }
        }

        public void UpdateDuration(float deltaTime)
        {
            if (DurationType == DurationType.Timed)
            {
                Duration -= deltaTime;

                if (Duration <= 0)
                {
                    RemoveConditionType = RemoveConditionType.AfterDuration;
                }
            }
        }
    }
}