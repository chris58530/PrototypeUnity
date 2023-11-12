using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    public int combo=0;

    private void Update()
    {
        TMP_Text comboText = GameObject.Find("ComboText").GetComponent<TMP_Text>();
        comboText.text = combo.ToString();
    }

    
}