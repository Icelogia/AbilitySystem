using System.Threading;
using Cysharp.Threading.Tasks;
using ShatteredIceStudio.AbilitySystem.Modificators;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public partial class AttributeSet
    {
        private CancellationTokenSource cancellationTokenSource;

        private void InitCancelationToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        private void CancelToken()
        {
            cancellationTokenSource.Cancel();
        }

        private void DisposeToken()
        {
            cancellationTokenSource.Dispose();
        }

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

        private async UniTask ApplyDelayed(Modificator modificator)
        {
            await UniTask.WaitForSeconds(modificator.Delay, cancellationToken: cancellationTokenSource.Token);
            ApplyStatusConditions(modificator);
            HandleModifications(modificator);
        }

        private async UniTask ApplyPeriod(Modificator modificator)
        {
            ApplyStatusConditions(modificator);

            for (int x = 0; x < modificator.Ticks; x++)
            {
                HandleModifications(modificator);
                await UniTask.WaitForSeconds(modificator.PeriodBetweenTicks, cancellationToken: cancellationTokenSource.Token);
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