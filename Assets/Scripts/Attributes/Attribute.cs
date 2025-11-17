using System;

namespace ICGames.AbilitySystem.Attributes
{
    /// <summary>
    /// Class used as wrapper to single attribute functionality. Doesn't contains the on change event.
    /// </summary>
    public class Attribute
    {
        private float attribute;
        private Action<float> SetAttributeAction;
        private Func<float> GetAttributeAction;

        public Attribute(float attribute, Action<float> setAttributeAction, Func<float> getAttributeAction)
        {
            this.attribute = attribute;
            SetAttributeAction = setAttributeAction;
            GetAttributeAction = getAttributeAction;
        }

        public float GetAttribute()
        {
            attribute = GetAttributeAction();
            return attribute;
        }

        public void SetAttribute(float newValue)
        {
            SetAttributeAction(newValue);
            attribute = newValue;
        }
    }
}