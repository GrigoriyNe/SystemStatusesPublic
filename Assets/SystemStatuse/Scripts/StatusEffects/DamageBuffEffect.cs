using UnityEngine;

namespace StatusGroup
{
    public class DamageBuffEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            float value = target.StatusManager.GetCurrenStack(this, target.Id) * ActiveValue;
            Debug.Log($"Урон усилен на {value}% для {target.Id}");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Усиление урона снято для {target.Id}");
    }
}