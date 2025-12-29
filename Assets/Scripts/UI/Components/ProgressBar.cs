using UnityEngine;
using UnityEngine.UI;

namespace ShatteredIceStudio.UI.Components
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image progressDisplay;

        public void SetSize(float normalizedSize)
        {
            normalizedSize = Mathf.Clamp01(normalizedSize);
            progressDisplay.fillAmount = normalizedSize;
        }
    }
}