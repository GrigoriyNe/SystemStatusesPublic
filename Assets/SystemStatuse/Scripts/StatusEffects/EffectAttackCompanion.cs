using UnityEngine;

namespace StatusGroup
{
    public class EffectAttackCompanion : StatusEffectBase
    {
        [SerializeField] private string _companionId;
        public override void Apply(BattleTargetProvider target)
        {
            if (target.InteractionInjection.GetPlayer().HaveCompanion(_companionId))
                Debug.Log($"Компаньон нанёс {ActiveValue} по {target.Id}");
        }
    }
}