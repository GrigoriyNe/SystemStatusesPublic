using UnityEngine;

namespace StatusGroup
{
    public class WeakenAbilitiesEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            float value = target.StatusManager.GetCurrenStack(this, target.Id) * ActiveValue;
            Debug.Log($"Ослаблены способности для {target.Id} на " +
                $"{value}%");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Ослабление для {target.Id} закончилось");
    }
}