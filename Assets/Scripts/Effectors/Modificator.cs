using UnityEngine;
using System.Collections.Generic;

namespace ICGames.AbilitySystem.Modificators
{
    [CreateAssetMenu(fileName = "Modificator", menuName = "Modificator/Init", order = 1)]
    public class Modificator : ScriptableObject
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
        [SerializeField] private List<Modification> modifications = new List<Modification>();

        public List<Modification> GetModifications()
        {
            return new List<Modification>(modifications);
        }
    }
}