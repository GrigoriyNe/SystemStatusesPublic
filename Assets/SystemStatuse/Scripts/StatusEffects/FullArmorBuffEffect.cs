using UnityEngine;

namespace StatusGroup
{
    public class FullArmorBuffEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target) =>
            Debug.Log($"Полностью брокирует урон для {target.Id}");

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Блокирование урона снято для {target.Id}");
    }
}