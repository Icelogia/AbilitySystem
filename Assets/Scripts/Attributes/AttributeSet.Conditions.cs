using System.Collections.Generic;
using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Effectors;

    public partial class AttributeSet
    {
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
            }
            else if (effector.Stackable)
            {
                tags.TryGetValue(effector.Tag, out var stack);
                tags[effector.Tag] = stack + 1;
            }
        }

        protected virtual void RemoveTags(Effector effector)
        {
            if(HasTag(effector.Tag))
            {
                tags.TryGetValue(effector.Tag, out var stack);
                stack = Mathf.Max(stack - 1, 0);
                if(stack == 0)
                    tags.Remove(effector.Tag);
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