using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public class Attribute{}

    public class Attribute<T> : Attribute
    {
        public delegate void AttributeEvent<V>(V oldValue, V newValue);

        public AttributeEvent<T> OnAttributeChanged;

        [SerializeField] private T attribute;

        public T Get()
        {
            return attribute;
        }

        public void Set(T newValue)
        {
            OnAttributeChanged?.Invoke(attribute, newValue);
            attribute = newValue;
        }
    }

    [System.Serializable]
    public class IntAttribute : Attribute<int> {}
    [System.Serializable]
    public class FloatAttribute : Attribute<float> {}
    [System.Serializable]
    public class Vector2Attribute : Attribute<Vector2> {}
}