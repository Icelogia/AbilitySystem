using UnityEngine;

namespace ShatteredIceStudio.AbilitySystem.Attributes
{
    public partial class AttributeSet : MonoBehaviour
    {
        private void Awake()
        {
            InitCancelationToken();
            InitAttributes();
        }

        private void OnDisable()
        {
            CancelToken();
        }

        private void OnDestroy()
        {
            DisposeToken();
        }
    }
}