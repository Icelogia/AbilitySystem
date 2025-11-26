using System;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public interface IAttribute
    {
        Type ValueType { get; }
        public object GetAttribute();
        public void SetAttribute(object newBalue);
    }

    public class Attribute<T> : IAttribute
    {
        private T attribute;
        private Action<T> SetAttributeAction;

        public Type ValueType => typeof(T);

        public object GetAttribute()
        {
            return attribute;
        }

        public void SetAttribute(object newValue) 
        {
            SetAttributeAction.Invoke((T)newValue);
            attribute = (T)newValue;
        }
    }
}