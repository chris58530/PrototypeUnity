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
    }
}