using System;
using UnityEngine;
using UnityEngine.UI;

namespace @_.Scripts.Player.Props
{
    public class AbilityValueUI : MonoBehaviour
    {
        [SerializeField] private Image valueImage;
        [SerializeField] private GameObject valueBG;

        private Color defaultColor;

        private void Start()
        {
            defaultColor = valueImage.color;
        }

        public void DisplayTime(float value, float max)
        {
            valueBG.SetActive(true);

            valueImage.fillAmount = value / max;

            // Debug.Log($"current ability time : {value}");

            if (value <= 0) StopUpdateTime();
        }

        public void ChangeDisplayColor(bool isChange)
        {
            if (isChange)
            {
                valueImage.color = Color.red;
            }
            else
            {
                valueImage.color = defaultColor;
            }
        }

        public void StopUpdateTime()
        {
            valueBG.SetActive(false);
        }
    }
}