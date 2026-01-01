using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Abilities
{
    public class AbilityController : MonoBehaviour
    {
        protected IAbilityHolder holder;

        public void SetAbilityHolder(IAbilityHolder newHolder)
        {
            holder = newHolder;
        }

        public virtual void Aim() {}

        public virtual void Cast() {}

        public virtual bool CanCast()
        {
            return true;
        }
    }
}
