using System.Collections.Generic;
using UnityEngine;

namespace StatusGroup
{
    public class InteractionInjection : MonoBehaviour
    {
        [SerializeField] private StatusSetter[] _enableStatusSetters;
        [SerializeField] private Transform _cardContainer;

        private List<BattleTargetProvider> _enemies = new List<BattleTargetProvider>();
        private List<BattleTargetProvider> _friendlyUnits = new List<BattleTargetProvider>();
        private List<BattleTargetProvider> _anotherUnits = new List<BattleTargetProvider>();

        private StatusSetter _activeSetter = null;

        private bool _waitInputAnyTaget = false;
        private bool _waitInputTarget = false;
        private bool _waitInputTargetBuff = false;

        private void Start()
        {
            foreach (StatusSetter item in _enableStatusSetters)
            {
                item.transform.SetParent(_cardContainer);

                switch (item.StatusProperty.TargetType)
                {
                    case BattleTargetType.Player:
                        item.StatusSetted += OnPlayerStatusSelected;
                        break;

                    case BattleTargetType.Enemy:
                        item.StatusSetted += OnEnemyTargetStatusSelected;
                        break;

                    case BattleTargetType.Without:
                        item.StatusSetted += OnWithoutTargetStatusSelected;
                        break;

                    case BattleTargetType.Any:
                        item.StatusSetted += OnAnytTargetStatusSelected;
                        break;
                }
            }
        }

        private void OnDestroy()
        {
            foreach (StatusSetter item in _enableStatusSetters)
            {
                switch (item.StatusProperty.TargetType)
                {
                    case BattleTargetType.Player:
                        item.StatusSetted -= OnPlayerStatusSelected;
                        break;

                    case BattleTargetType.Enemy:
                        item.StatusSetted -= OnEnemyTargetStatusSelected;
                        break;

                    case BattleTargetType.Without:
                        item.StatusSetted -= OnWithoutTargetStatusSelected;
                        break;

                    case BattleTargetType.Any:
                        item.StatusSetted -= OnAnytTargetStatusSelected;
                        break;
                }
            }

            foreach (BattleTargetProvider item in _friendlyUnits)
                item.OnSelected -= OnFrendlySelected;

            foreach (BattleTargetProvider item in _enemies)
                item.OnSelected -= OnEnemySelected;
        }

        public BattleTargetProvider GetPlayer()
        {
            foreach (BattleTargetProvider item in _friendlyUnits)
            {
                if (item.Type == BattleTargetType.Player)
                    return _friendlyUnits[0];
            }

            return null;
        }

        public void SetFrendlyUnit(BattleTargetProvider provider)
        {
            _friendlyUnits.Add(provider);
            provider.OnSelected += OnFrendlySelected;
        }

        public void SetEnemyUnit(BattleTargetProvider provider)
        {
            _enemies.Add(provider);
            provider.OnSelected += OnEnemySelected;
        }

        public void SetAnotherUnit(BattleTargetProvider value) =>
            _anotherUnits.Add(value);

        public BattleTargetProvider GetСompanion(BattleTargetProvider target)
        {
            List<BattleTargetProvider> copyList;

            if (target.Type == BattleTargetType.Player
                || target.Type == BattleTargetType.Companion)
            {
                if (_friendlyUnits.Count > 1)
                {
                    copyList = new List<BattleTargetProvider>(_friendlyUnits);
                    copyList.Remove(target);

                    return copyList[UnityEngine.Random.Range(0, copyList.Count)];
                }
                else
                {
                    return null;
                }
            }
            else if(target.Type == BattleTargetType.Enemy)
            {
                if (_enemies.Count > 1)
                {
                    copyList = new List<BattleTargetProvider>(_enemies);
                    copyList.Remove(target);

                    return copyList[UnityEngine.Random.Range(0, copyList.Count)];
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
        private void OnAnytTargetStatusSelected(StatusSetter setter)
        {
            if (_activeSetter != null) return;
            {
                Debug.Log("Выберите цель");

                _waitInputAnyTaget = true;
                _activeSetter = setter;
            }
        }

        private void OnWithoutTargetStatusSelected(StatusSetter setter) =>
            setter.Activate(GetPlayer());

        private void OnAnySelected(BattleTargetProvider provider)
        {
            if (_waitInputAnyTaget && _activeSetter != null)
            {
                _activeSetter.Activate(provider);
                _waitInputAnyTaget = false;
                _activeSetter = null;
            }
        }

        private void OnFrendlySelected(BattleTargetProvider provider)
        {
            if (_waitInputAnyTaget)
            {
                OnAnySelected(provider);
                return;
            }

            if (_waitInputTargetBuff && _activeSetter != null)
            {
                _activeSetter.Activate(provider);
                _waitInputTargetBuff = false;
                _activeSetter = null;
            }
        }

        private void OnEnemySelected(BattleTargetProvider provider)
        {
            if (_waitInputAnyTaget)
            {
                OnAnySelected(provider);
                return;
            }


            if (_waitInputTarget && _activeSetter != null)
            {
                _activeSetter.Activate(provider);
                _waitInputTarget = false;
                _activeSetter = null;
            }
        }

        private void OnEnemyTargetStatusSelected(StatusSetter setter)
        {
            if (_activeSetter != null) return;

            Debug.Log("Выберите врага");

            _waitInputTarget = true;
            _activeSetter = setter;
        }

        private void OnPlayerStatusSelected(StatusSetter setter)
        {
            if (_activeSetter != null) return;

            if (_friendlyUnits.Count > 1)
            {
                Debug.Log("Выберите юнита");

                _waitInputTargetBuff = true;
                _activeSetter = setter;
            }
            else
            {
                setter.Activate(_friendlyUnits[0]);
            }
        }
    }
}