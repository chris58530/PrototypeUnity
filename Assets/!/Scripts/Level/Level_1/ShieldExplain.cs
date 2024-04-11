using System.Collections;
using System.Collections.Generic;
using _.Scripts.Player;
using _.Scripts.Player.Props;
using UnityEngine;

public class ShieldExplain : MonoBehaviour
{
    public void ShowExplainTimeline()
    {
        TimeLineManager.Instance.PlayTimeLine(6);
        FindObjectOfType<AbilitySystem>().isBlockInsert = false;
        Destroy(gameObject);
    }
}