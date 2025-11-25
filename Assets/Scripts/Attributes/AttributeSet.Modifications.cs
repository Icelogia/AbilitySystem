using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Modificators;
    using Effectors;

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

        public void Apply(Effector effector)
        {
            switch (effector.Timing)
            {
                case Timing.Instant:
                    ApplyInstant(effector);
                    break;
                case Timing.Delayed:
                    ApplyDelayed(effector);
                    break;
                case Timing.Period:
                    ApplyPeriod(effector);
                    break;
            }
        }

        private void ApplyInstant(Effector effector)
        {
            ApplyStatusConditions(effector);
            HandleModifications(effector);
        }

        private async UniTask ApplyDelayed(Effector effector)
        {
            await UniTask.WaitForSeconds(effector.Delay, cancellationToken: cancellationTokenSource.Token);
            ApplyStatusConditions(effector);
            HandleModifications(effector);
        }

        private async UniTask ApplyPeriod(Effector effector)
        {
            ApplyStatusConditions(effector);

            for (int x = 0; x < effector.Ticks; x++)
            {
                HandleModifications(effector);
                await UniTask.WaitForSeconds(effector.PeriodBetweenTicks, cancellationToken: cancellationTokenSource.Token);
            }

            RemoveStatusConditions(effector);
        }

        private void ApplyStatusConditions(Effector effector)
        {
            if (effector.Stackable || !HasCondition(effector.Condition))
               ApplyCondition(effector.Condition);

            if (effector.Stackable || !HasResistance(effector.Resistance))
                ApplyResistance(effector.Resistance);

            if (effector.Stackable || !HasImmunity(effector.Immunity))
                ApplyImmunity(effector.Immunity);
        }

        private void RemoveStatusConditions(Effector effector)
        {
            RemoveCondition(effector.Condition);
            RemoveResistance(effector.Resistance);
            RemoveImmunity(effector.Immunity);
        }

        private void HandleModifications(Effector effector)
        {
            float modificationMultiplier = 1;

            if (HasResistance(effector.Condition))
                modificationMultiplier = 0.5f;

            if (HasImmunity(effector.Condition))
                modificationMultiplier = 0f;

            foreach (Modificator mod in effector.GetModifications())
            {
                mod.ApplyModification(this, modificationMultiplier);
            }
        }
    }
}