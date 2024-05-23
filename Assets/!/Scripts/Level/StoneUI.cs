using System;
using _.Scripts.Ability;
using _.Scripts.Event;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class StoneUI : MonoBehaviour
{
    private Image[] allImages;
    [SerializeField] private Image stoneWallHpImage;

    private void OnEnable()
    {
        ShowImage(false);
        PlayerActions.endPlayerUseBrokeWallAbility += ScaleShowImage;
    }

    private void OnDisable()
    {
        PlayerActions.endPlayerUseBrokeWallAbility -= ScaleShowImage;
    }

    public void ScaleShowImage(bool show)
    {
        if (!show)
        {
            FadeOutAndHideImage();
            return;
        }

        ShowImage(true);

        foreach (var image in allImages)
        {
            // image.transform.localScale = new Vector3(currentScale, currentScale, currentScale);
            image.transform.DOScale(1.25f, 0.2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    image.transform.DOScale(.1f, 0.15f)
                        .SetEase(Ease.OutQuad)
                        .OnComplete(() =>
                        {
                            image.transform.DOScale(1, 0.1f)
                                .SetEase(Ease.OutQuad);
                        });
                });
        }
    }

    public void FadeOutAndHideImage()
    {
        foreach (var image in allImages)
        {
            // scale form 0 to 1.2
            image.transform.DOScale(1.3f, 0.1f).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    image.transform.DOScale(0, 0.15f).SetEase(Ease.OutQuad).OnComplete(() =>
                    {
                        image.enabled = false;
                    });
                });
        }
    }

    public void ShowImage(bool show)
    {
        allImages = GetComponentsInChildren<Image>();
        foreach (var image in allImages)
        {
            image.enabled = show;
        }
    }


    public void UpdateHpImage(float currentHp, float maxHp)
    {
        foreach (var image in allImages)
        {
            // scale form 0 to 1.2
            image.transform.DOScale(.8f, 0.1f).SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    image.transform.DOScale(1, 0.05f)
                        .SetEase(Ease.OutQuad);
                });
        }

        stoneWallHpImage.fillAmount = currentHp / maxHp;
    }
}