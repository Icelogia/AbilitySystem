using UnityEngine;

namespace ShatteredIceStudio.UI
{
    using Components;

    public class HUDManager : Singleton<HUDManager>
    {
        [field: SerializeField] public ProgressBar TargetHPBar { get; private set; }
    }
}