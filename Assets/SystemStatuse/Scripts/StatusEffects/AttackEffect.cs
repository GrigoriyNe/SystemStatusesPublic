using UnityEngine;

namespace StatusGroup
{
    public class AttackEffect : StatusEffectBase
    {
        public override void Apply(BattleTargetProvider target)
        {
            target.StatusManager.UpdateAfteAttackPlayer(target);
            Debug.Log($"Нанесёно {ActiveValue} урона, цель: {target.Id}");
        }
    }
}