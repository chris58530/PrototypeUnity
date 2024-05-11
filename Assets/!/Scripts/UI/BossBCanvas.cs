using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BossBCanvas : MonoBehaviour
{
    [SerializeField] private Image[] headImages;
    [SerializeField] private Image[] leftHandImages;

    [SerializeField] private Image[] rightHandImages;

    [SerializeField] private Image[] shadowImages;

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

                if (damageCount == 0)
                {
                    shadowImages[0].gameObject.SetActive(false);
                }


                break;
            case BodyType.RightHand:

                foreach (var rightHand in rightHandImages)
                {
                    rightHand.gameObject.SetActive(false);
                }

                for (int i = 0; i < rightHandImages.Length; i++)
                {
                    if (i == damageCount)
                    {
                        rightHandImages[i].gameObject.SetActive(true);
                    }
                }

                if (damageCount == 0)
                {
                    shadowImages[1].gameObject.SetActive(false);
                }

                break;
            case BodyType.Head:
                damageCount -= 1;

                foreach (var headImage in headImages)
                {
                    headImage.gameObject.SetActive(false);
                }

                for (int i = 0; i < headImages.Length; i++)
                {
                    if (i == damageCount)
                    {
                        headImages[i].gameObject.SetActive(true);
                    }
                }

                if (damageCount < 0)
                {
                    headImages[0].gameObject.SetActive(true);
                }

                break;
        }
    }

    public void ShakingImage(BodyType bodyType)
    {
        switch (bodyType)
        {
            case BodyType.LeftHand:
                foreach (var leftHand in leftHandImages)
                {
                    if (leftHand.gameObject.activeSelf)
                        StartCoroutine(ShakeCoroutine(leftHand.gameObject, 0.2f));
                }

                break;
            case BodyType.RightHand:
                foreach (var rightHand in rightHandImages)
                {
                    if (rightHand.gameObject.activeSelf)
                        StartCoroutine(ShakeCoroutine(rightHand.gameObject, 0.2f));
                }


                break;
            case BodyType.Head:
                foreach (var headImage in headImages)
                {
                    if (headImage.gameObject.activeSelf)
                        StartCoroutine(ShakeCoroutine(headImage.gameObject, 0.2f));
                }

                break;
        }
    }

    IEnumerator ShakeCoroutine(GameObject shakeModel, float shakeDuration)
    {
        Debug.Log("shaking" + shakeModel.name);
        Vector3 originalPos = shakeModel.transform.localPosition;
        float elapsedTime = 0f; // 添加一個變量來跟踪經過的時間
        float shakeRange = 10f; // 設定震動速度
        while (elapsedTime < shakeDuration) // 使用經過的時間作為迴圈條件
        {
            // 透過添加隨機噪音更新位置
            shakeModel.transform.localPosition = originalPos + new Vector3(Random.Range(-shakeRange, shakeRange),
                Random.Range(-shakeRange, shakeRange), 0);

            elapsedTime += Time.deltaTime; // 增加經過的時間
            yield return null; // 讓出控制權並繼續下一幀的執行
        }

        shakeModel.transform.localPosition = originalPos; // 重置位置到初始值
        yield return null;
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

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}