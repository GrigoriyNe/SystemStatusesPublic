using System.Collections.Generic;
using UnityEngine;

namespace StatusGroup
{
    [CreateAssetMenu(fileName = "StatusProperty",
    menuName = "SwipeItems/StatusProperty",
    order = 51)]
    public class StatusProperty : ScriptableObject
    {
        [SerializeField] private string _id;
        [SerializeField] private string _name;
        [SerializeField] private StatusType _type;
        [SerializeField] private IBattleTarget _target;
        [SerializeField] private BattleTargetType _targetType;
        [SerializeField] private int _valueCurentStacks;
        [SerializeField] private int _maxStacks;
        [SerializeField] private string _effectDescription;
        [SerializeField] private bool _canStack;
        [SerializeField] private bool _canBeSpent;
        [SerializeField] private bool _canBeMoved;
        [SerializeField] private DurationType _durationType;
        [SerializeField] private RemoveConditionType _removeConditionType;
        [SerializeField] private TriggerLogicType _triggerLogicType;

        [SerializeField] private float _duration;
        [SerializeField] private int _turnsRemaining;
        [SerializeField] private List<StatusEffectBase> _effects;

        public string Id => _id;
        public string Name => _name;
        public StatusType Type => _type;
        public IBattleTarget Target => _target;
        public BattleTargetType TargetType => _targetType;
        public int CurrentStacks => _valueCurentStacks;
        public int MaxStacks => _maxStacks;
        public string EffectDescription => _effectDescription;
        public bool CanStack => _canStack;
        public bool CanBeSpent => _canBeSpent;
        public bool CanBeMoved => _canBeMoved;
        public DurationType DurationType => _durationType;
        public RemoveConditionType RemoveConditionType => _removeConditionType;
        public TriggerLogicType TriggerLogicType => _triggerLogicType;
        public float Duration => _duration;
        public int TurnsRemaining => _turnsRemaining;
        public List<StatusEffectBase> Effects => _effects;
    }
}