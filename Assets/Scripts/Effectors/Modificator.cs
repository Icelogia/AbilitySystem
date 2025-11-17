namespace ICGames.AbilitySystem.Effectors
{
    using Attributes;

    [System.Serializable]
    public class Modificator
    {
        public AttributeType attribute;
        public Modification modification;
        public float change;
    }
}