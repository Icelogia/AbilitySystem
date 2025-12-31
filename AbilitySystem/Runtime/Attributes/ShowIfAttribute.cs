using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class ShowIfAttribute : PropertyAttribute
{
    public string comparedPropertyName { get; private set; }
    public object comparedValue { get; private set; }
    public DisablingType disablingType { get; private set; }

    public enum DisablingType
    {
        ReadOnly = 2,
        DontDraw = 3
    }

    public ShowIfAttribute(string comparedPropertyName, object comparedValue, DisablingType disablingType = DisablingType.DontDraw)
    {
        this.comparedPropertyName = comparedPropertyName;
        this.comparedValue = comparedValue;
        this.disablingType = disablingType;
    }
}
