using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBCanvas : MonoBehaviour
{
    [SerializeField] private Image[] headImages;
    [SerializeField] private Image[] leftHandImages;

    [SerializeField] private Image[] rightHandImages;

    public void SetBreakImage(BodyType bodyType, int count)
    {
        switch (bodyType)
        {
            case BodyType.Head:
                for (int i = 0; i < headImages.Length; i++)
                {
                    if (i < count)
                    {
                        headImages[i].enabled = true;
                    }
                    else
                    {
                        headImages[i].enabled = false;
                    }
                }
                break;
            case BodyType.LeftHand:
                break;
            case BodyType.RightHand:
                break;
          
        }
    }
}