using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SparkleType
{
    Normal,
    Crystal,
    Shield
}

public class SparkleEffect : MonoBehaviour
{
    [SerializeField] private GameObject normalSparkle;
    [SerializeField] private GameObject crystalSparkle;
    [SerializeField] private GameObject shieldSparkle;


    public static Action<SparkleType, Vector3, Quaternion> onPlaySparkleEffect;

    private void OnEnable()
    {
        onPlaySparkleEffect += PlaySparkle;
    }

    private void OnDisable()
    {
        onPlaySparkleEffect -= PlaySparkle;
    }

    public void PlaySparkle(SparkleType type, Vector3 position, Quaternion rotation)
    {
        switch (type)
        {
            case SparkleType.Normal:

                GameObject normalSparkleObject = Instantiate(normalSparkle.gameObject, position, rotation);
                normalSparkleObject.transform.parent = null;
                Destroy(normalSparkleObject, 1f);
                break;
            case SparkleType.Crystal:
                GameObject crystalSparkleObject = Instantiate(crystalSparkle.gameObject, position, rotation);
                crystalSparkleObject.transform.parent = null;
                Destroy(crystalSparkleObject, 1f);
                break;
            case SparkleType.Shield:
                GameObject sparkleObject = Instantiate(shieldSparkle.gameObject, position, rotation);
                sparkleObject.transform.parent = null;
                Destroy(sparkleObject, 1f);
                break;
        }
    }
}