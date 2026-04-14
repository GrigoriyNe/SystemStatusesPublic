using UnityEngine;

namespace StatusGroup
{
    public class MoveStatusEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            Status newStatus = target.StatusManager.GetMovableStatus(target);
            BattleTargetProvider provider = target.InteractionInjection.GetСompanion(target);

            if (newStatus == null)
            {
                Debug.Log($"На {target.Id} нет эффектов для перемещения");
                return;
            }

            if (provider == null)
            {
                Debug.Log($"Нет цели для перемещения");
                return;
            }

            target.StatusManager.MoveStatus(newStatus, provider);
            Debug.Log($"Статус {newStatus.Name} перемещён на {provider.Id}");
        }
    }
}