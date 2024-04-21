using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class RhinoCartTimeline : MonoBehaviour
{
    private PlayableDirector _playableDirector;

    [Tooltip("active rhino model")] [SerializeField]
    private GameObject[] rhinoModel;

    void Start()
    {
        _playableDirector = GetComponent<PlayableDirector>();
    }

    void LateUpdate()
    {
        if (_playableDirector.time >= 1) return;

        foreach (var obj in rhinoModel)
        {
            if (!obj.activeSelf)
                obj.SetActive(true);
        }
    }
}