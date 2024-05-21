using UnityEngine;
using UnityEngine.UI;

public class StoneUI : MonoBehaviour
{
    [SerializeField] private Image stoneWallHpImage;


    public void UpdateHpImage(float currentHp, float maxHp)
    {
        stoneWallHpImage.fillAmount = currentHp / maxHp;
    }
}