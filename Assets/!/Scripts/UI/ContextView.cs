using UnityEngine;
using UnityEngine.UI;

namespace _.Scripts.UI
{
    public class ContextView : MonoBehaviour
    {
       [SerializeField] private Image hpImage;
       [SerializeField] private Image abilityImage;

       public void UpdateHp(float hp,float max)
       {
           hpImage.fillAmount = hp / max;
       }   public void UpdateAbility(float hp,float max)
       {
           abilityImage.fillAmount = hp / max;
       }
    }
}