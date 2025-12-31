using UnityEngine;
using System.Collections.Generic;

namespace ShatteredIceStudio.AbilitySystem.Effectors
{
    using Modificators;

    [CreateAssetMenu(fileName = "Effector", menuName = "Ability System/Effector", order = 1)]
    public class Effector : ScriptableObject
    {
        [Header("Timing")]
        [SerializeField] private Timing timing = Timing.Instant;
        /// <summary>
        /// Used for delayed timing.
        /// </summary>
        [SerializeField, ShowIf(nameof(timing), Timing.Delayed)] private float delay = 0f;
        /// <summary>
        /// Used for period timing.
        /// </summary>
        [SerializeField, ShowIf(nameof(timing), Timing.Period)] private float periodBetweenTicks = 0f;
        [SerializeField, ShowIf(nameof(timing), Timing.Period)] private int ticks = 1;

        public Timing Timing => timing;
        public float Delay => delay;
        public float PeriodBetweenTicks => periodBetweenTicks;
        public int Ticks => ticks;

        [Header("Tags")]
        [field: SerializeField] public Tag Tag { get; private set; } = Tag.None;
        [field: SerializeField] public bool Stackable { get; private set; } = false;

        [Space]
        [SerializeField] private List<Modificator> modifications = new List<Modificator>();

        public List<Modificator> GetModifications()
        {
            return new List<Modificator>(modifications);
        }
    }
}