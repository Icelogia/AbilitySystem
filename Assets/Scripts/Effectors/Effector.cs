using UnityEngine;
using System.Collections.Generic;
using ShatteredIceStudio.AbilitySystem.Modificators;

namespace ShatteredIceStudio.AbilitySystem.Effectors
{
    [CreateAssetMenu(fileName = "Effector", menuName = "Ability System/Effector", order = 1)]
    public class Effector : ScriptableObject
    {
        [Header("Timing")]
        [field: SerializeField] public Timing Timing { get; private set; } = Timing.Instant;
        /// <summary>
        /// Used for delayed timing.
        /// </summary>
        [field: SerializeField] public float Delay { get; private set; } = 0f;
        /// <summary>
        /// Used for period timing.
        /// </summary>
        [field: SerializeField] public float PeriodBetweenTicks { get; private set; } = 0f;
        [field: SerializeField] public int Ticks { get; private set; } = 1;

        [Header("Status Conditions")]
        [field: SerializeField] public StatusCondition Condition { get; private set; } = StatusCondition.None;
        [field: SerializeField] public StatusCondition Resistance { get; private set; } = StatusCondition.None;
        [field: SerializeField] public StatusCondition Immunity { get; private set; } = StatusCondition.None;
        [field: SerializeField] public bool Stackable { get; private set; } = false;

        [Space]
        [SerializeField] private List<Modificator> modifications = new List<Modificator>();

        public List<Modificator> GetModifications()
        {
            return new List<Modificator>(modifications);
        }
    }
}