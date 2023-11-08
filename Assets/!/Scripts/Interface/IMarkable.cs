using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMarkable
{
    public bool CanPull { get; set; }
    public bool CanDash { get; set; }
    public bool HasMarked { get; set; }
    public Vector3 PullDirection { get; set; }
    public void Pull();
    public void SetVisualizePullDirection(Vector3 direction);
    public void SetMark(bool isMark);
}