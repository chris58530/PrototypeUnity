using System;
using System.Collections;
using System.Collections.Generic;
using _.Scripts.Enemy;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class BossBBase : Enemy
{
    [Header("Base setting")] [SerializeField]
    private int hpMax;

    private int currentHp;

    //左手
    private readonly int _breakLeftHandHp = 75;
    [SerializeField] private BossBBody[] leftHand;
    private int _currentleftHandBreakHp = 3;

    //右手
    private readonly int _breakRightHandHp = 25;
    [SerializeField] private BossBBody[] rightHand;
    private int _currentRightHandBreakHp = 3;

    //頭
    private readonly int _breakHeadHandHp = 0;
    [SerializeField] private BossBBody[] head;
    private int _currentHeadHandBreakHp = 3;

    [Header("UI Setting")] [SerializeField]
    private Image hpValue;


    [Header("Event")] [SerializeField] private UnityEvent onTakeDamagedEvent;
    [SerializeField] private UnityEvent onDiedEvent;
    public static Action<int> onBodyTakeDamage;
    public static Action<BodyType, BossBBody> onBodyBreakDamage;
    public static Action onBodyDied;

    private void Start()
    {
        //初始化血量
        currentHp = hpMax;
        UpdateHpValue(0);
    }

    private void OnEnable()
    {
        onBodyTakeDamage += OnTakeDamage;
        onBodyDied += OnDied;
        onBodyBreakDamage += OnBodyGetBreak;
    }


    public void OnTakeDamage(int value)
    {
        onTakeDamagedEvent?.Invoke();

        foreach (var hand in leftHand)
        {
            if (hand.canBreak && !hand.isBroken)
            {
                Debug.Log("砍在護頓上 目標為 :  leftHand");
                if (hand.gameObject.activeSelf)
                    hand.HitBreak(_currentleftHandBreakHp); //維持原來的護頓值
                return;
            }
        }

        foreach (var hand in rightHand)
        {
            if (hand.canBreak && !hand.isBroken)
            {
                Debug.Log("砍在護頓上  目標為 : rightHand");
                if (hand.gameObject.activeSelf)
                    hand.HitBreak(_currentRightHandBreakHp); //維持原來的護頓值
                return;
            }
        }

        foreach (var hand in head)
        {
            if (hand.canBreak && !hand.isBroken)
            {
                Debug.Log("砍在護頓上 目標為 : head");
                if (hand.gameObject.activeSelf)
                    hand.HitBreak(_currentHeadHandBreakHp); //維持原來的護頓值
                return;
            }
        }

        UpdateHpValue(value);

        foreach (var hand in leftHand)
        {
            if (currentHp <= _breakLeftHandHp && !hand.isBroken && !hand.canBreak)
            {
                hand.canBreak = true;
                hand.OpenBreak(); //護頓首次登場
            }
        }

        foreach (var hand in rightHand)
        {
            if (currentHp <= _breakRightHandHp && !hand.isBroken && !hand.canBreak)
            {
                hand.canBreak = true;
                hand.OpenBreak(); //護頓首次登場
            }
        }

        foreach (var hand in head)
        {
            if (currentHp <= _breakHeadHandHp && !hand.isBroken && !hand.canBreak)
            {
                hand.canBreak = true;
                hand.OpenBreak(); //護頓首次登場
            }
        }
    }

    public void UpdateHpValue(int value)
    {
        currentHp -= value;

        hpValue.fillAmount = (float)currentHp / hpMax;
    }

    public void OnBodyGetBreak(BodyType currentBodyType, BossBBody currentBody)
    {
        switch (currentBodyType)
        {
            case BodyType.LeftHand:
                _currentleftHandBreakHp--;

                currentBody.HitBreak(_currentleftHandBreakHp);

                if (_currentleftHandBreakHp <= 0)
                {
                    foreach (var hand in leftHand)
                    {
                        hand.isBroken = true;
                    }
                }

                break;
            case BodyType.RightHand:
                _currentRightHandBreakHp--;

                currentBody.HitBreak(_currentRightHandBreakHp);

                if (_currentRightHandBreakHp <= 0)
                {
                    foreach (var hand in rightHand)
                    {
                        hand.isBroken = true;
                    }
                }

                break;
            case BodyType.Head:
                _currentHeadHandBreakHp--;

                currentBody.HitBreak(_currentHeadHandBreakHp);

                if (_currentHeadHandBreakHp <= 0)
                {
                    OnDied();
                    foreach (var hand in head)
                    {
                        hand.isBroken = true;
                    }
                }

                break;
        }
    }

    public void OnDied()
    {
        Destroy(gameObject);
        onDiedEvent?.Invoke();
    }


    private void OnDisable()
    {
        onBodyTakeDamage -= OnTakeDamage;
        onBodyDied -= OnDied;
        onBodyBreakDamage -= OnBodyGetBreak;
    }
}