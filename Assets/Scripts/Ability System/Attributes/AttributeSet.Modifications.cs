using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Modificators;
    using Effectors;

    public partial class AttributeSet
    {
        private CancellationTokenSource cancellationTokenSource;

        protected void InitCancelationToken()
        {
            cancellationTokenSource = new CancellationTokenSource();
        }

        protected void CancelToken()
        {
            cancellationTokenSource.Cancel();
        }

        protected void DisposeToken()
        {
            cancellationTokenSource.Dispose();
        }

        public void Apply(Effector effector, AttributeSet owner)
        {
            if (!initialized)
                return;

            switch (effector.Timing)
            {
                case Timing.Instant:
                    ApplyInstant(effector, owner);
                    break;
                case Timing.Delayed:
                    ApplyDelayed(effector, owner);
                    break;
                case Timing.Period:
                    ApplyPeriod(effector, owner);
                    break;
            }
        }

        public void Apply(EffectorContext context)
        {
            if (!initialized)
                return;

            switch (context.Effector.Timing)
            {
                case Timing.Instant:
                    ApplyInstant(context.Effector, context.Owner);
                    break;
                case Timing.Delayed:
                    ApplyDelayed(context.Effector, context.Owner);
                    break;
                case Timing.Period:
                    ApplyPeriod(context.Effector, context.Owner);
                    break;
            }
        }

        protected void ApplyInstant(Effector effector, AttributeSet owner)
        {
            ApplyTags(effector);
            HandleModifications(effector, owner);
        }

        protected async UniTask ApplyDelayed(Effector effector, AttributeSet owner)
        {
            await UniTask.WaitForSeconds(effector.Delay, cancellationToken: cancellationTokenSource.Token);
            ApplyTags(effector);
            HandleModifications(effector, owner);
        }

        protected async UniTask ApplyPeriod(Effector effector, AttributeSet owner)
        {
            ApplyTags(effector);

            for (int x = 0; x < effector.Ticks; x++)
            {
                HandleModifications(effector, owner);
                await UniTask.WaitForSeconds(effector.PeriodBetweenTicks, cancellationToken: cancellationTokenSource.Token);
            }

            RemoveTags(effector);
        }

        protected virtual void HandleModifications(Effector effector, AttributeSet owner)
        {
            foreach (Modificator mod in effector.GetModifications())
            {
                mod.ApplyModification(this, owner);
            }
        }
    }
}