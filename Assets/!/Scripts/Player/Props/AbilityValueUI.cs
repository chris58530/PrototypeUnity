using UnityEngine;
using UnityEngine.UI;

namespace @_.Scripts.Player.Props
{
    public class AbilityValueUI : MonoBehaviour
    {
        [SerializeField] private Image valueImage;
        [SerializeField] private GameObject valueBG;


        public void DisplayTime(float value, float max)
        {
            valueBG.SetActive(true);

            valueImage.fillAmount = value / max;

            // Debug.Log($"current ability time : {value}");
            
            if (value <= 0) StopUpdateTime();
        }

        public void StopUpdateTime()
        {
            valueBG.SetActive(false);
        }
    }
}