using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public interface IMarkable
{
    public bool GetMark { get; set; }
    public void Mark();
}