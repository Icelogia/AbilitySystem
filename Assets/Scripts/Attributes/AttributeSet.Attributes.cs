using System.Collections.Generic;

namespace ICGames.AbilitySystem.Attributes
{
    public delegate void OnAttributeChanged<T>(T oldValue, T newValue);

    /// <summary>
    /// Class containing all the attributes of the characters and type mapping.
    /// </summary>
    public partial class AttributeSet
    {
        public Dictionary<AttributeType, Attribute> Attributes = new Dictionary<AttributeType, Attribute>();

        private void Awake()
        {
            Attributes.Add(AttributeType.Health, new Attribute(health, SetHealth, GetHealth));
            Attributes.Add(AttributeType.MaxHealth, new Attribute(maxHealth, SetMaxHealth, GetMaxHealth));
            Attributes.Add(AttributeType.Damage, new Attribute(damage, SetDamage, GetDamage));
        }

        //Health
        private float health;
        public OnAttributeChanged<float> OnHealthChanged;

        public void SetHealth(float newValue)
        {
            if (health != newValue)
            {
                OnHealthChanged?.Invoke(health, newValue);
                health = newValue;
            }
        }

        public float GetHealth()
        {
            return health;
        }

        //MaxHealth
        private float maxHealth;
        public OnAttributeChanged<float> OnMaxHealthChanged;

        public void SetMaxHealth(float newValue)
        {
            if (maxHealth != newValue)
            {
                OnMaxHealthChanged?.Invoke(maxHealth, newValue);
                maxHealth = newValue;
            }
        }

        public float GetMaxHealth()
        {
            return maxHealth;
        }

        //Damage
        private float damage;
        public OnAttributeChanged<float> OnDamageChanged;

        public void SetDamage(float newValue)
        {
            OnDamageChanged?.Invoke(damage, newValue);
            damage = newValue;
        }

        public float GetDamage()
        {
            return damage;
        }
    }
}