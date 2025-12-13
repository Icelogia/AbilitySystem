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

        public void Apply(Effector effector)
        {
            if (!initialized)
                return;

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

        protected void ApplyInstant(Effector effector)
        {
            ApplyTags(effector);
            HandleModifications(effector);
        }

        protected async UniTask ApplyDelayed(Effector effector)
        {
            await UniTask.WaitForSeconds(effector.Delay, cancellationToken: cancellationTokenSource.Token);
            ApplyTags(effector);
            HandleModifications(effector);
        }

        protected async UniTask ApplyPeriod(Effector effector)
        {
            ApplyTags(effector);

            for (int x = 0; x < effector.Ticks; x++)
            {
                HandleModifications(effector);
                await UniTask.WaitForSeconds(effector.PeriodBetweenTicks, cancellationToken: cancellationTokenSource.Token);
            }

            RemoveTags(effector);
        }

        protected virtual void HandleModifications(Effector effector)
        {
            foreach (Modificator mod in effector.GetModifications())
            {
                mod.ApplyModification(this, 1);
            }
        }
    }
}