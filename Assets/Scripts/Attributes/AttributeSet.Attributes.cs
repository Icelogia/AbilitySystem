using System.Collections.Generic;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    /// <summary>
    /// Class containing all the attributes of the characters and type mapping.
    /// </summary>
    public partial class AttributeSet
    {
        public readonly Dictionary<AttributeType, Attribute> Attributes = new Dictionary<AttributeType, Attribute>();

        protected virtual void InitAttributes()
        {
            Attributes.Add(AttributeType.Health, Health);
            Attributes.Add(AttributeType.MaxHealth, MaxHealth);
            Attributes.Add(AttributeType.Speed, Speed);
        }

        public readonly IntAttribute Health = new IntAttribute();
        public readonly IntAttribute MaxHealth = new IntAttribute();
        public readonly FloatAttribute Speed = new FloatAttribute();
    }
}