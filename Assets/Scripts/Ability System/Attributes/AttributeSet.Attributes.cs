using System.Collections.Generic;
using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    using Effectors;

    /// <summary>
    /// Class containing all the attributes of the characters and type mapping.
    /// </summary>
    public partial class AttributeSet
    {
        public readonly Dictionary<AttributeType, Attribute> Attributes = new Dictionary<AttributeType, Attribute>();

        public readonly IntAttribute Health = new IntAttribute();
        public readonly IntAttribute MaxHealth = new IntAttribute();
        public readonly FloatAttribute Speed = new FloatAttribute();


        [SerializeField] private Effector initAttributesEffector;


        private bool initialized = false;

        public void Initialize(Effector initData)
        {
            initialized = true;
            Apply(initData);
        }

        protected virtual void InitAttributes()
        {
            Attributes.Add(AttributeType.Health, Health);
            Attributes.Add(AttributeType.MaxHealth, MaxHealth);
            Attributes.Add(AttributeType.Speed, Speed);

            if(initAttributesEffector != null)
                Initialize(initAttributesEffector);
        }
    }
}