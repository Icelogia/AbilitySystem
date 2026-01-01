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

        [Tooltip("Initiali data set during initialization. It is not required but recommended.")]
        [SerializeField] private Effector initAttributesEffector;

        [Space]
        [Header("Attributes")]
        public IntAttribute Health;
        public IntAttribute MaxHealth;
        public FloatAttribute Speed;

        private bool initialized = false;

        public void Initialize(Effector initData)
        {
            initialized = true;

            if(initAttributesEffector != null)
                Apply(initData, this);
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