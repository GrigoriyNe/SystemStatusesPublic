using UnityEngine;

namespace StatusGroup
{
    public class EffectGiveItem : StatusEffectBase
    {
        private const string KeyId = "GiveKey";

        public override void Apply(BattleTargetProvider target)
        {
            target.StatusManager.AddStack(KeyId + target.Id);

            Debug.Log($"Новый предмет для {target.Id}");
            Debug.Log($"Всего {target.StatusManager.GetCurrenStack(KeyId + target.Id)} ключей");
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Осталость {target.StatusManager.GetCurrenStack(KeyId + target.Id)}");
    }
}