using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour
{
    public static bool isOpened = false;
    private Animator _animator;
    [SerializeField] private GameObject attackGameObject;
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        attackGameObject.SetActive(false);
        isOpened = false;
    }

    public void OpenChest()
    {
        if (!isOpened)
            StartCoroutine(OpenChestRoutine());
        else StartCoroutine(ChestAttackRoutine());
    }

    IEnumerator OpenChestRoutine()
    {
        isOpened = true;

        yield return new WaitForSeconds(0.5f);
        _animator.Play("Normal_Open");
        yield return null;
    }

    IEnumerator ChestAttackRoutine()
    {
        Vector3 originalPos = transform.position;
        Vector3 playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        yield return new WaitForSeconds(0.5f);

        _animator.Play("Monster_Attack");
        yield return new WaitForSeconds(0.1f);
        while (Vector3.Distance(transform.position, playerPos) > 6f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, playerPos, 0.7f);
            yield return null;
        }
        yield return new WaitForSeconds(0.55f);

        
        attackGameObject.SetActive(true);

        yield return new WaitForSeconds(0.25f);


        while (Vector3.Distance(transform.position, originalPos) > .1f)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, originalPos, 0.5f);
            yield return null;
        }
        attackGameObject.SetActive(false);
        _animator.CrossFade(Animator.StringToHash("Monster_Spin"), 0.2f);

        yield return new WaitForSeconds(2f);

        _animator.CrossFade(Animator.StringToHash("Monster_Idle"), 0.2f);

        yield return new WaitForSeconds(8f);
        _animator.CrossFade(Animator.StringToHash("None"), 0.2f);


        yield return null;
    }
}