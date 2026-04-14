using System;
using UnityEngine;

namespace StatusGroup
{
    public class EffectCreateCompanion : StatusEffectBase
    {
        private const string KeyId = "GiveKey";

        [SerializeField] private float _offsetParent;
        [SerializeField] private string _idCompanion;
        [SerializeField] private BattleTargetProvider _companionPrefab;

        public override void Apply(BattleTargetProvider target)
        {
            if (target.InteractionInjection.GetPlayer().HaveCompanion(_idCompanion))
            {
                Debug.Log("Можно создать одного компаньона этого типа");

                return;
            }

            if (target.StatusManager.GetCurrenStack(KeyId + target.Id) >= ActiveValue)
            {
                _companionPrefab.Init(target.StatusManager);
                BattleTargetProvider companion = Instantiate(_companionPrefab);
                target.StatusManager.SpendStatus(KeyId + target.Id, Convert.ToInt32(ActiveValue));

                Vector2 positionCrate = new Vector2(
                    target.transform.position.x - _offsetParent,
                    target.transform.position.y);

                companion.transform.position = positionCrate;
                target.InteractionInjection.GetPlayer().AddCompanion(companion.Id);

                Debug.Log($"Компаньон {companion.Id} создан");
            }
        }

        public override void Remove(BattleTargetProvider target) =>
            Debug.Log($"Осталость " +
                $"{target.StatusManager.GetCurrenStack(KeyId + target.Id)} ключей");
    }
}