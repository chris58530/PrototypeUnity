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
    [SerializeField] private ParticleSystem normalSparkle;
    [SerializeField] private ParticleSystem crystalSparkle;
    [SerializeField] private ParticleSystem shieldSparkle;

    
    public static Action<SparkleType,Vector3,Quaternion> onPlaySparkleEffect;

    private void OnEnable()
    {
        onPlaySparkleEffect+= PlaySparkle;
    }

    private void OnDisable()
    {
        onPlaySparkleEffect-= PlaySparkle;
    }
    public void PlaySparkle(SparkleType type,Vector3 position ,Quaternion rotation)
    {
        switch (type)
        {
            case SparkleType.Normal:
                var o = normalSparkle.gameObject;
                o.transform.position = position;
                o.transform.rotation = rotation;
                normalSparkle.Play();
                break;
            case SparkleType.Crystal:
                var gameObject1 = crystalSparkle.gameObject;
                gameObject1.transform.position = position;
                gameObject1.transform.rotation = rotation;
                crystalSparkle.Play();
                break;
            case SparkleType.Shield:
                var shieldObj = shieldSparkle.gameObject;
                shieldObj.transform.position = position;
                shieldObj.transform.rotation = rotation;
                shieldSparkle.Play();
                break;
        }
    }
}