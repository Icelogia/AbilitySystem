using System.Threading;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Modificators;
    using Effectors;
    

    public partial class AttributeSet
    {
        private CancellationTokenSource cancellationTokenSource;

#if UNITY_EDITOR
        /// <summary>
        /// List containing history of applied effectors.
        /// </summary>
        public readonly List<EffectorContext> EffectorsHistory = new List<EffectorContext>();

        private const int historyThreshold = 10;

        /// <summary>
        /// List containg all currently active effectors.
        /// </summary>
        public readonly List<EffectorContext> ActiveEffectors = new List<EffectorContext>();
#endif

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
#if UNITY_EDITOR
            EffectorContext context = new EffectorContext
            {
                Effector = effector,
                Owner = owner
            };

            AddToHistory(context);
#endif

            ApplyTags(effector);
            HandleModifications(effector, owner);
        }

        protected async UniTask ApplyDelayed(Effector effector, AttributeSet owner)
        {
#if UNITY_EDITOR
            EffectorContext context = new EffectorContext
            {
                Effector = effector,
                Owner = owner
            };

            AddToHistory(context);
            AddToActiveEffectors(context);
#endif

            await UniTask.WaitForSeconds(effector.Delay, cancellationToken: cancellationTokenSource.Token);
            ApplyTags(effector);
            HandleModifications(effector, owner);

#if UNITY_EDITOR
            RemoveFromActiveEffectors(context);
#endif
        }

        protected async UniTask ApplyPeriod(Effector effector, AttributeSet owner)
        {
#if UNITY_EDITOR
            EffectorContext context = new EffectorContext
            {
                Effector = effector,
                Owner = owner
            };

            AddToHistory(context);
            AddToActiveEffectors(context);
#endif

            ApplyTags(effector);

            for (int x = 0; x < effector.Ticks; x++)
            {
                HandleModifications(effector, owner);
                await UniTask.WaitForSeconds(effector.PeriodBetweenTicks, cancellationToken: cancellationTokenSource.Token);
            }

            RemoveTags(effector);

#if UNITY_EDITOR
            RemoveFromActiveEffectors(context);
#endif
        }

        protected virtual void HandleModifications(Effector effector, AttributeSet owner)
        {
            foreach (Modificator mod in effector.GetModifications())
            {
                mod.ApplyModification(this, owner);
            }
        }

#if UNITY_EDITOR
        private void AddToHistory(EffectorContext context)
        {
            if (EffectorsHistory.Count >= historyThreshold)
                EffectorsHistory.RemoveAt(0);

            EffectorsHistory.Add(context);
        }

        private void AddToActiveEffectors(EffectorContext context)
        {
            ActiveEffectors.Add(context);
        }

        private void RemoveFromActiveEffectors(EffectorContext context)
        {
            ActiveEffectors.Remove(context);
        }
#endif
    }
}