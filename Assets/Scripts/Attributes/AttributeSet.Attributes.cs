using System.Collections.Generic;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public delegate void OnAttributeChanged<T>(T oldValue, T newValue);

    /// <summary>
    /// Class containing all the attributes of the characters and type mapping.
    /// </summary>
    public partial class AttributeSet
    {
        public Dictionary<AttributeType, IAttribute> Attributes = new Dictionary<AttributeType, IAttribute>();

        private void InitAttributes()
        {
            Attributes.Add(AttributeType.Health, health);
            Attributes.Add(AttributeType.MaxHealth, maxHealth);
            Attributes.Add(AttributeType.Speed, speed);
        }

        private Attribute<float> health = new Attribute<float>();
        private Attribute<float> maxHealth = new Attribute<float>();
        private Attribute<float> speed = new Attribute<float>();
    }
}