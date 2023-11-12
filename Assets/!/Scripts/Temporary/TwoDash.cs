using System.Collections;
using System.Collections.Generic;
using _.Scripts.Temporary;
using TMPro;
using UnityEngine;

public class TwoDash : PlayerBehaviourSimple
{
    private bool isOnBeat = false; //節點布林

    public int combo;
    public TMP_Text comboText;
    public float dashDistance = 10f;
    public float dashDuration = 0.5f;
    private bool isDashing = false;
    [SerializeField] private int attackValue;


    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Q) && !isDashing)
        {
            Dash();
            bool onBeat = BeatManager.onBeat;
            if (onBeat)
            {
                combo += 1;
                comboText.text = combo.ToString();
            }
            else
            {
                combo = 0;
                comboText.text = combo.ToString();
            }
        }
    }

    void Dash()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        LayerMask mask = 1 << LayerMask.NameToLayer("DashDetect");
        var targetPosition = Vector3.zero;
        if (Physics.Raycast(ray, out RaycastHit hit, 1000, mask))
        {
            Debug.DrawLine(ray.origin, hit.point);
            targetPosition = hit.point;
            targetPosition.y = transform.position.y;
            Debug.Log(hit.point);
        }

        Vector3 dashDirection = (targetPosition - transform.position).normalized;
        StartCoroutine(PerformDash(dashDirection));
    }

    IEnumerator PerformDash(Vector3 dashDirection)
    {
        isDashing = true;
        Vector3 endPosition = transform.position + dashDirection * dashDistance;

        float elapsedTime = 0f;

        while (elapsedTime < dashDuration)
        {
            transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / dashDuration);
            elapsedTime += Time.deltaTime;        transform.LookAt(dashDirection);

            yield return null;
        }

        isDashing = false;
    }

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<EnemyTempCold>(out var damageObj))
        {
            damageObj.OnTakeDamage(attackValue);
        }
    }
}