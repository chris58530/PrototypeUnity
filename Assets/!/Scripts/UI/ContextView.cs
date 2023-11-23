using UnityEngine;
using UnityEngine.UI;

namespace _.Scripts.UI
{
    public class ContextView : MonoBehaviour
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private Image skillImage;

        public void UpdateHp(float current, float max)
        {
            hpImage.fillAmount = current / max;
        }

        public void UpdateSkill(float current, float max)
        {
            skillImage.fillAmount = current / max;
        }
    }
}