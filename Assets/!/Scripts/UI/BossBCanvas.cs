using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossBCanvas : MonoBehaviour
{
    [SerializeField] private Image[] headImages;
    [SerializeField] private Image[] leftHandImages;

    [SerializeField] private Image[] rightHandImages;

    private void Start()
    {
        ResetAllBreakImage();

        SetBreakImage(BodyType.Head, 3);
        SetBreakImage(BodyType.LeftHand, 3);
        SetBreakImage(BodyType.RightHand, 3);
    }

    public void SetBreakImage(BodyType bodyType, int damageCount)
    {
        switch (bodyType)
        {
            case BodyType.Head:
                damageCount -= 1;

                foreach (var leftHand in headImages)
                {
                    leftHand.gameObject.SetActive(false);
                }

                for (int i = 0; i <= headImages.Length; i++)
                {
                    if (i == damageCount)
                    {
                        headImages[i].gameObject.SetActive(true);
                    }
                }

                break;
            case BodyType.LeftHand:

                foreach (var leftHand in leftHandImages)
                {
                    leftHand.gameObject.SetActive(false);
                }

                for (int i = 0; i < leftHandImages.Length; i++)
                {
                    if (i == damageCount)
                    {
                        leftHandImages[i].gameObject.SetActive(true);
                    }
                }

                if (damageCount < 0)
                {
                    leftHandImages[leftHandImages.Length].gameObject.SetActive(true);
                
                }

                break;
            case BodyType.RightHand:

                foreach (var leftHand in rightHandImages)
                {
                    leftHand.gameObject.SetActive(false);
                }

                for (int i = 0; i < rightHandImages.Length; i++)
                {
                    if (i == damageCount)
                    {
                        rightHandImages[i].gameObject.SetActive(true);
                    }
                }
                if (damageCount < 0)
                {
                    rightHandImages[leftHandImages.Length].gameObject.SetActive(true);
                  
                }


                break;
        }
    }

    public void ResetAllBreakImage()
    {
        foreach (var leftHand in headImages)
        {
            leftHand.gameObject.SetActive(false);
        }

        foreach (var leftHand in leftHandImages)
        {
            leftHand.gameObject.SetActive(false);
        }

        foreach (var leftHand in rightHandImages)
        {
            leftHand.gameObject.SetActive(false);
        }
    }

 
}