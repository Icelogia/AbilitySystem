using UnityEngine;

namespace ICGames.AbilitySystem.Modificators
{
    using Attributes;

    [System.Serializable]
    public class Modification
    {
        [field: SerializeField] public AttributeType Attribute { get; private set; }
        [field: SerializeField] public ModificationType ModificationType { get; private set; }
        [field: SerializeField] public float Change { get; private set; }
    }
}