using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.AbilityControllers
{
    using ShatteredIceStudio.Core;

    public class AbilityController : MonoBehaviour
    {
        [SerializeField] protected Transform spawnSlot;
        [SerializeField] protected Animator animator;
        [SerializeField] protected AnimationClip overrideActivateClip = null;

        [SerializeField] protected float attackCooldown = 1f;
        [SerializeField] protected Duration shootDelay;

        protected float attackTimer = 0;

        protected IAbilityHolder holder;

        private void Awake()
        {
            shootDelay.IsFinished = true;
            shootDelay.OnEndOfDuration += ShootStart;
        }

        protected virtual void Update()
        {
            if(attackTimer > 0)
            {
                attackTimer -= Time.deltaTime;
            }

            shootDelay.UpdateTimer(Time.deltaTime);
        }

        public void SetAbilityHolder(IAbilityHolder newHolder)
        {
            holder = newHolder;
        }

        public virtual void Shoot() 
        {
            shootDelay.Restart();
            attackTimer = attackCooldown;
            if (animator)
                animator.SetTrigger("shoot");
        }

        protected virtual void ShootStart() {}

        public virtual bool CanUse()
        {
            return attackTimer <= 0;
        }

        public AnimationClip GetOverrideActivateAnimationClip()
        {
            return overrideActivateClip;
        }
    }
}
