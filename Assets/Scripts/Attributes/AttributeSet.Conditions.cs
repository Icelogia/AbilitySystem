using System.Collections.Generic;

namespace ICGames.AbilitySystem.Attributes
{
    /// <summary>
    /// Class containing all the attributes of the characters and type mapping.
    /// </summary>
    public partial class AttributeSet
    {
        /// <summary>
        /// Status Condition, stacks
        /// </summary>
        private Dictionary<StatusCondition, int> immunity = new Dictionary<StatusCondition, int>();
        private Dictionary<StatusCondition, int> resistance = new Dictionary<StatusCondition, int>();
        private Dictionary<StatusCondition, int> conditions = new Dictionary<StatusCondition, int>();

        public void ApplyImmunity(StatusCondition condition)
        {
            if (condition != StatusCondition.None)
            {
                if (immunity.ContainsKey(condition))
                    immunity[condition]++;
                else
                    immunity.Add(condition, 1);
            }
        }

        public void RemoveImmunity(StatusCondition condition)
        {
            if (immunity.ContainsKey(condition))
            {
                immunity[condition]--;

                if (immunity[condition] == 0)
                    immunity.Remove(condition);
            }
        }

        public bool HasImmunity(StatusCondition condition)
        {
            if (immunity.ContainsKey(condition))
                return immunity[condition] != 0;

            return false;
        }

        public void ApplyResistance(StatusCondition condition)
        {
            if (condition != StatusCondition.None)
            {
                if (resistance.ContainsKey(condition))
                    resistance[condition]++;
                else
                    resistance.Add(condition, 1);
            }
        }

        public void RemoveResistance(StatusCondition condition)
        {
            if (resistance.ContainsKey(condition))
            {
                resistance[condition]--;

                if (resistance[condition] == 0)
                    resistance.Remove(condition);
            }
        }

        public bool HasResistance(StatusCondition condition)
        {
            if (resistance.ContainsKey(condition))
                return resistance[condition] != 0;

            return false;
        }

        public void ApplyCondition(StatusCondition condition)
        {
            if (condition != StatusCondition.None)
            {
                if (conditions.ContainsKey(condition))
                    conditions[condition]++;
                else
                    conditions.Add(condition, 1);
            }
        }

        public void RemoveCondition(StatusCondition condition)
        {
            if (conditions.ContainsKey(condition))
            {
                conditions[condition]--;

                if (conditions[condition] == 0)
                    conditions.Remove(condition);
            }
        }

        public bool HasCondition(StatusCondition condition)
        {
            if (conditions.ContainsKey(condition))
                return conditions[condition] != 0;

            return false;
        }
    }
}