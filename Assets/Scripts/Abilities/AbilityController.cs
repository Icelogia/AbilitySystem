using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.AbilityControllers
{
    using ShatteredIceStudio.Core;

    public class AbilityController : MonoBehaviour
    {
        [SerializeField] protected Transform spawnSlot;

        [SerializeField] protected float castCooldown = 1f;
        [SerializeField] protected Duration castDelay;

        protected float castTimer = 0;

        protected IAbilityHolder holder;

        private void Awake()
        {
            castDelay.IsFinished = true;
            castDelay.OnEndOfDuration += EndOfCooldown;
        }

        protected virtual void Update()
        {
            if(castTimer > 0)
            {
                castTimer -= Time.deltaTime;
            }

            castDelay.UpdateTimer(Time.deltaTime);
        }

        public void SetAbilityHolder(IAbilityHolder newHolder)
        {
            holder = newHolder;
        }

        public virtual void Aim() {}

        public virtual void Cast() 
        {
            castDelay.Restart();
            castTimer = castCooldown;
        }

        protected virtual void EndOfCooldown() {}

        public virtual bool CanCast()
        {
            return castTimer <= 0;
        }
    }
}
