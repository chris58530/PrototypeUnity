using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugCanvas : MonoBehaviour
{
    [SerializeField] private GameObject sceneSelectorPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (sceneSelectorPanel.activeSelf == false)
                sceneSelectorPanel.SetActive(true);
            else
                sceneSelectorPanel.SetActive(false);
        }
    }

    public void SelectScene(int num)
    {
        SceneManager.LoadScene(num);
    }
}