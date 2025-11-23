using System.Collections;
using ICGames.AbilitySystem.Modificators;
using UnityEngine;

namespace ICGames.AbilitySystem.Attributes
{
    public partial class AttributeSet
    {
        public void Apply(Modificator modificator)
        {
            switch (modificator.Timing)
            {
                case Timing.Instant:
                    ApplyInstant(modificator);
                    break;
                case Timing.Delayed:
                    ApplyDelayed(modificator);
                    break;
                case Timing.Period:
                    ApplyPeriod(modificator);
                    break;
            }
        }

        private void ApplyInstant(Modificator modificator)
        {
            ApplyStatusConditions(modificator);
            HandleModifications(modificator);
        }

        private void ApplyDelayed(Modificator modificator)
        {
            StartCoroutine(DelayedEffector(modificator));
        }

        private IEnumerator DelayedEffector(Modificator modificator)
        {
            yield return new WaitForSeconds(modificator.Delay);
            ApplyStatusConditions(modificator);
            HandleModifications(modificator);
        }

        private void ApplyPeriod(Modificator modificator)
        {
            StartCoroutine(PeriodEffector(modificator));
        }

        private IEnumerator PeriodEffector(Modificator modificator)
        {
            ApplyStatusConditions(modificator);

            for (int x = 0; x < modificator.Ticks; x++)
            {
                HandleModifications(modificator);
                yield return new WaitForSeconds(modificator.PeriodBetweenTicks);
            }

            RemoveStatusConditions(modificator);
        }

        private void ApplyStatusConditions(Modificator modificator)
        {
            if (modificator.Stackable || !HasCondition(modificator.Condition))
               ApplyCondition(modificator.Condition);

            if (modificator.Stackable || !HasResistance(modificator.Resistance))
                ApplyResistance(modificator.Resistance);

            if (modificator.Stackable || !HasImmunity(modificator.Immunity))
                ApplyImmunity(modificator.Immunity);
        }

        private void RemoveStatusConditions(Modificator modificator)
        {
            RemoveCondition(modificator.Condition);
            RemoveResistance(modificator.Resistance);
            RemoveImmunity(modificator.Immunity);
        }

        private void HandleModifications(Modificator modificator)
        {
            float modificationMultiplier = 1;

            if (HasResistance(modificator.Condition))
                modificationMultiplier = 0.5f;

            if (HasImmunity(modificator.Condition))
                modificationMultiplier = 0f;

            foreach (Modification mod in modificator.GetModifications())
            {
                Attribute attribute = Attributes[mod.Attribute];
                float attributeValue = attribute.GetAttribute();

                switch (mod.ModificationType)
                {
                    case ModificationType.Add:
                        attributeValue += mod.Change * modificationMultiplier;
                        break;
                    case ModificationType.Multiply:
                        attributeValue *= mod.Change * modificationMultiplier;
                        break;
                    case ModificationType.Custom:
                        break;
                    default:
                        break;
                }

                attribute.SetAttribute(attributeValue);
            }
        }
    }
}