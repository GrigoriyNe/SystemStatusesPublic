using UnityEngine;

namespace StatusGroup
{
    public class ArmorBuffEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            float value = target.StatusManager.GetCurrenStack(this, target.Id) * ActiveValue;
            Debug.Log($"Защита усилена на {value}% для {target.Id}");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Усиление защиты снято для {target.Id}");
    }
}