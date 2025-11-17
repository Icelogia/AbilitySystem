using UnityEngine;
using System.Collections.Generic;

namespace ICGames.AbilitySystem.Effectors
{
    using Attributes;
    using System.Collections;

    [CreateAssetMenu(fileName = "Effector", menuName = "Effectors/Init", order = 1)]
    public class Effector : ScriptableObject
    {
        [Header("Timing")]
        [SerializeField] private Timing timing = Timing.Instant;
        /// <summary>
        /// Used for delayed timing.
        /// </summary>
        [SerializeField] private float delay = 0f;
        /// <summary>
        /// Used for period timing.
        /// </summary>
        [SerializeField] private float periodBetweenTicks = 0f;
        [SerializeField] private int ticks = 1;

        [Header("Status Conditions")]
        [SerializeField] private StatusCondition condition;
        [SerializeField] private StatusCondition resistance = StatusCondition.None;
        [SerializeField] private StatusCondition immunity = StatusCondition.None;
        [SerializeField] private bool stackable = false;

        [Space]
        [SerializeField] private List<Modificator> modifications = new List<Modificator>();

        public void Apply(AttributeSet attributeSet)
        {
            switch (timing) 
            {
                case Timing.Instant:
                    ApplyInstant(attributeSet);
                    break;
                case Timing.Delayed: 
                    ApplyDelayed(attributeSet);
                    break;
                case Timing.Period:
                    ApplyPeriod(attributeSet);
                    break;
            }
        }

        private void ApplyInstant(AttributeSet attributeSet)
        {
            ApplyStatusConditions(attributeSet);
            HandleModifications(attributeSet);
        }

        private void ApplyDelayed(AttributeSet attributeSet)
        {
            EffectorManager.Instance.StartCoroutine(DelayedEffector(attributeSet));
        }

        private IEnumerator DelayedEffector(AttributeSet attributeSet)
        {
            yield return new WaitForSeconds(delay);
            ApplyStatusConditions(attributeSet);
            HandleModifications(attributeSet);
        }

        private void ApplyPeriod(AttributeSet attributeSet)
        {
            EffectorManager.Instance.StartCoroutine(PeriodEffector(attributeSet));
        }

        private IEnumerator PeriodEffector(AttributeSet attributeSet)
        {
            ApplyStatusConditions(attributeSet);

            for (int x = 0; x < ticks; x++)
            {
                HandleModifications(attributeSet);
                yield return new WaitForSeconds(periodBetweenTicks);
            }

            RemoveStatusConditions(attributeSet);
        }

        private void ApplyStatusConditions(AttributeSet attributeSet)
        {
            if(stackable || !attributeSet.HasCondition(condition))
                attributeSet.ApplyCondition(condition);

            if (stackable || !attributeSet.HasResistance(resistance))
                attributeSet.ApplyResistance(resistance);

            if (stackable || !attributeSet.HasImmunity(immunity))
                attributeSet.ApplyImmunity(immunity);
        }

        private void RemoveStatusConditions(AttributeSet attributeSet)
        {
            attributeSet.RemoveCondition(condition);
            attributeSet.RemoveResistance(resistance);
            attributeSet.RemoveImmunity(immunity);
        }

        private void HandleModifications(AttributeSet attributeSet)
        {
            float modificationMultiplier = 1;

            if (attributeSet.HasResistance(condition))
                modificationMultiplier = 0.5f;

            if (attributeSet.HasImmunity(condition))
                modificationMultiplier = 0f;

            foreach (Modificator mod in modifications)
            {
                Attribute attribute = attributeSet.Attributes[mod.attribute];
                float attributeValue = attribute.GetAttribute();

                switch (mod.modification)
                {
                    case Modification.Add:
                        attributeValue += mod.change * modificationMultiplier;
                        break;
                    case Modification.Multiply:
                        attributeValue *= mod.change * modificationMultiplier;
                        break;
                    case Modification.Custom:
                        break;
                    default:
                        break;
                }

                attribute.SetAttribute(attributeValue);
            }
        }
    }
}
