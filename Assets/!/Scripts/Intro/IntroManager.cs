using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using TimeSpan = MagicaCloth2.TimeSpan;

public class IntroManager : MonoBehaviour
{
    [SerializeField] private GameObject destroyObject;

    private void Start()
    {
        int damage = 0;
        destroyObject.OnTriggerEnterAsObservable().Subscribe(collider1 =>
        {
            if (collider1.gameObject.CompareTag("Sword"))
            {
                damage++;
                if (damage >= 10)
                    Destroy(destroyObject);
            }
        }).AddTo(this);

        destroyObject.OnDestroyAsObservable().Subscribe(_ => { SwitchScene(); }).AddTo(this);
    }

    public void SwitchScene()
    {
        SceneManager.LoadScene("level1");
    }
}