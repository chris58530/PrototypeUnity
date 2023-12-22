using System;
using UnityEngine;
using UnityEngine.UI;

namespace _.Scripts.UI
{
    public class PlayerUIView : MonoBehaviour
    {
        [SerializeField] private Image hpImage;
        [SerializeField] private Image skillImage;
        [SerializeField] private Image[] sheildImage;
        [SerializeField] private Image[] emptySheildImage;

        public void UpdateHp(float current, float max)
        {
            hpImage.fillAmount = current / max;
        }

        public void UpdateLevel(float current, float max)
        {
            skillImage.fillAmount = current / max;
        }

        public void UpdateSheld(int current)
        {
            switch (current)
            {
                case 0:
                    ResetSheild();

                    break;
                case 1:
                    ResetSheild();
                    for (int i = 0; i < 1; i++)
                    {
                        sheildImage[i].enabled = true;
                        emptySheildImage[i].enabled = false;
                    }

                    break;
                case 2:
                    ResetSheild();
                    for (int i = 0; i < 2; i++)
                    {
                        sheildImage[i].enabled = true;
                        emptySheildImage[i].enabled = false;
                    }

                    break;
                case 3:
                    for (int i = 0; i < 3; i++)
                    {
                        sheildImage[i].enabled = true;
                        emptySheildImage[i].enabled = false;
                    }

                    break;
            }
        }

        private void ResetSheild()
        {
            for (int i = 0; i < sheildImage.Length; i++)
            {
                sheildImage[i].enabled = false;
                emptySheildImage[i].enabled = true;
            }
        }
    }
}