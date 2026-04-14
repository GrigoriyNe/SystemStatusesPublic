using UnityEngine;

namespace StatusGroup
{
    public class StatusEffectBase : MonoBehaviour
    {
        [SerializeField] private float _activeValue;

        public float ActiveValue =>
            _activeValue;

        public virtual void Apply(BattleTargetProvider target) { }
        public virtual void Remove(BattleTargetProvider target) { }
        public virtual void OnTrigger(BattleTargetProvider target, TriggerLogicType triggerType) { }

        public void SetActiveValue(float activeValue) =>
            _activeValue = activeValue;
    }
}