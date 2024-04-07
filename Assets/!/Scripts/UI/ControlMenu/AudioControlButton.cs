
using UnityEngine;

public class AudioControlButton : MonoBehaviour
{
    [SerializeField] private GameObject audioPanel;

    public void ShowAudioControlPanel(bool open)
    {
        audioPanel.SetActive(open);
    }
}