using UnityEngine;

public class BasicContorlButton : MonoBehaviour
{
    [SerializeField] private GameObject controlImage;

    public void ShowControlImage(bool open)
    {
        controlImage.SetActive(open);
    }
}