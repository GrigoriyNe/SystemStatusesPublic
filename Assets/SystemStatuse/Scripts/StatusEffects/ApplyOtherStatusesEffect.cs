using UnityEngine;

namespace StatusGroup
{
    public class ApplyOtherStatusesEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            Status status = new Status
            (
                target.OtherEffectForChange.Id,
                target.OtherEffectForChange.Name,
                target.OtherEffectForChange.Type,
                target,
                target.OtherEffectForChange.TargetType,
                target.OtherEffectForChange.CurrentStacks,
                target.OtherEffectForChange.MaxStacks,
                target.OtherEffectForChange.EffectDescription,
                target.OtherEffectForChange.CanStack,
                target.OtherEffectForChange.CanBeSpent,
                target.OtherEffectForChange.CanBeMoved,
                target.OtherEffectForChange.DurationType,
                target.OtherEffectForChange.RemoveConditionType,
                target.OtherEffectForChange.TriggerLogicType,
                target.OtherEffectForChange.Duration,
                target.OtherEffectForChange.TurnsRemaining,
                target.OtherEffectForChange.Effects
            );

            target.StatusManager.AddStatus(status);
            Debug.Log($"Применён новый статус для {target.Id}");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Статус для {target.Id} снят");
    }
}