using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public partial class AttributeSet : MonoBehaviour
    {
        protected virtual void Awake()
        {
            InitCancelationToken();
            InitAttributes();
        }

        protected virtual void OnDisable()
        {
            CancelToken();

#if UNITY_EDITOR
            ClearActiveEffectors();
#endif
        }

        protected virtual void OnDestroy()
        {
            DisposeToken();
        }
    }
}