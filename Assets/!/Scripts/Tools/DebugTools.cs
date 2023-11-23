using TMPro;
using UnityEngine;

namespace _.Scripts.Tools
{
    public class DebugTools : MonoBehaviour
    {
        public static void StateText(string t)
        {
            TMP_Text tmp = GameObject.Find("StateText").GetComponent<TMP_Text>();
            tmp.text = t;
        }

        public static void HpText(float t)
        {
            TMP_Text tmp = GameObject.Find("HPText").GetComponent<TMP_Text>();
            tmp.text = t.ToString();
        }

        public static void SkillText(float t)
        {
            TMP_Text tmp = GameObject.Find("SkillText").GetComponent<TMP_Text>();
            tmp.text = t.ToString();
        }
    }
}