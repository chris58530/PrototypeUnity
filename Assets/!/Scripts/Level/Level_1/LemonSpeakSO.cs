using UnityEngine;

[CreateAssetMenu(fileName = "LemonSpeak", menuName = "LemonSpeak/LemonSpeak", order = 0)]
public class LemonSpeakSO : ScriptableObject
{
    public string[] dialog;
}

public class LemoneSpeakManager : MonoBehaviour
{
    [SerializeField]private LemonSpeakSO[] lemonSpeakSo;
    
    
    
}