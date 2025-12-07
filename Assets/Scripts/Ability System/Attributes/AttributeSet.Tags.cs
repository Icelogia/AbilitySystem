using System.Collections.Generic;
using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Effectors;

    public partial class AttributeSet
    {
        public delegate Tag TagChange();

        /// <summary>
        /// Called when first stack of tag has been added.
        /// </summary>
        public TagChange OnTagAdded;

        /// <summary>
        /// Called when tag's stack has been increased.
        /// </summary>
        public TagChange OnTagIncreased;

        /// <summary>
        /// Called when tag's stack has been decreased.
        /// </summary>
        public TagChange OnTagDecreased;

        /// <summary>
        /// Called when last tag's stack has been removed.
        /// </summary>
        public TagChange OnTagRemoved;

        /// <summary>
        /// Tags, stacks
        /// </summary>
        protected Dictionary<Tag, int> tags = new Dictionary<Tag, int>();

        protected virtual void ApplyTags(Effector effector)
        {
            if (effector.Tag == Tag.None)
                return;

            if(!HasTag(effector.Tag))
            { 
                tags.Add(effector.Tag, 1);
                OnTagAdded?.Invoke();
                OnTagIncreased?.Invoke();
            }
            else if (effector.Stackable)
            {
                tags.TryGetValue(effector.Tag, out var stack);
                tags[effector.Tag] = stack + 1;
                OnTagIncreased?.Invoke();
            }
        }

        protected virtual void RemoveTags(Effector effector)
        {
            if(HasTag(effector.Tag))
            {
                tags.TryGetValue(effector.Tag, out var stack);
                stack = Mathf.Max(stack - 1, 0);
                OnTagDecreased?.Invoke();

                if (stack == 0)
                {
                    tags.Remove(effector.Tag);
                    OnTagRemoved?.Invoke();
                }
                else
                    tags[effector.Tag] -= 1;
            }
        }

        public bool HasTag(Tag condition)
        {
            if (tags.ContainsKey(condition))
                return tags[condition] != 0;

            return false;
        }
    }
}