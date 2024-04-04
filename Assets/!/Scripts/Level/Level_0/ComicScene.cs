using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComicScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimeLineManager.Instance.PlayTimeLine(0);
    }

  
}
