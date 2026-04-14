using UnityEngine;

namespace StatusGroup
{
    public class EnhanceAbilitiesEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            float value = target.StatusManager.GetCurrenStack(this, target.Id) * ActiveValue;
            Debug.Log($"Усилены способности для {target.Id} на {value}%");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Усиление для {target.Id} закончилось");
    }
}