using UnityEngine;

namespace ICGames.AbilitySystem.Effectors
{
    using Core;

    public class EffectorManager : Singleton<EffectorManager>
    {
        public void StopAllEffectors()
        {
            StopAllCoroutines();
        }
    }
}