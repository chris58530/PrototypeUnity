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
    private readonly int _breakLeftHandHp = 85;
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
    private BossBCanvas _bossBCanvas;

    private void Start()
    {
        //初始化血量
        _bossBCanvas = FindObjectOfType<BossBCanvas>();

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

        foreach (var leftBody in leftHand)
        {
            if (leftBody.thisBreakTurn && !leftBody.isBroken)
            {
                Debug.Log("砍在護頓上 目標為 :  leftHand");
                _bossBCanvas.ShakingImage(BodyType.LeftHand);
                if (leftBody.gameObject.activeSelf)
                    leftBody.HitBreak(_currentleftHandBreakHp);
                return;
            }
        }

        foreach (var rightBody in rightHand)
        {
            if (rightBody.thisBreakTurn && !rightBody.isBroken)
            {
                Debug.Log("砍在護頓上  目標為 : rightHand");
                _bossBCanvas.ShakingImage(BodyType.RightHand);
                if (rightBody.gameObject.activeSelf)
                    rightBody.HitBreak(_currentRightHandBreakHp); //維持原來的護頓值
                return;
            }
        }

        foreach (var headBody in head)
        {
            if (headBody.thisBreakTurn && !headBody.isBroken)
            {
                Debug.Log("砍在護頓上 目標為 : head");
                _bossBCanvas.ShakingImage(BodyType.Head);
                if (headBody.gameObject.activeSelf)
                    headBody.HitBreak(_currentHeadHandBreakHp); //維持原來的護頓值
                return;
            }
        }

        UpdateHpValue(value);

        foreach (var leftBody in leftHand)
        {
            if (currentHp <= _breakLeftHandHp && !leftBody.isBroken && !leftBody.thisBreakTurn)
            {
                leftBody.thisBreakTurn = true;
                // hand.OpenBreak(); //護頓首次登場
            }
        }

        foreach (var rightBody in rightHand)
        {
            if (currentHp <= _breakRightHandHp && !rightBody.isBroken && !rightBody.thisBreakTurn)
            {
                rightBody.thisBreakTurn = true;
                // hand.OpenBreak(); //護頓首次登場
            }
        }

        foreach (var headBody in head)
        {
            if (currentHp <= _breakHeadHandHp && !headBody.isBroken && !headBody.thisBreakTurn)
            {
                headBody.thisBreakTurn = true;
                // hand.OpenBreak(); //護頓首次登場
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
                _bossBCanvas.SetBreakImage(BodyType.LeftHand, _currentleftHandBreakHp);

                _bossBCanvas.ShakingImage(BodyType.LeftHand);

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
                _bossBCanvas.SetBreakImage(BodyType.RightHand, _currentRightHandBreakHp);
                _bossBCanvas.ShakingImage(BodyType.RightHand);

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

                _bossBCanvas.SetBreakImage(BodyType.Head, _currentHeadHandBreakHp);
                _bossBCanvas.ShakingImage(BodyType.Head);

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
        // Destroy(gameObject);
        onDiedEvent?.Invoke();
    }


    private void OnDisable()
    {
        onBodyTakeDamage -= OnTakeDamage;
        onBodyDied -= OnDied;
        onBodyBreakDamage -= OnBodyGetBreak;
    }
}