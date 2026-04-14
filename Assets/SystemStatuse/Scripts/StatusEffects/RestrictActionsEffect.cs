using UnityEngine;

namespace StatusGroup
{
    public class RestrictActionsEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            float value = target.StatusManager.GetCurrenStack(this, target.Id) * ActiveValue;
            Debug.Log($"Ограничено действие для {target.Id} на " +
                $"{value}%");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Ограничение для {target.Id} снято");
    }
}