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
        }

        protected virtual void OnDestroy()
        {
            DisposeToken();
        }
    }
}