using UnityEngine;

public interface IPullable
{
    public void Pull();
    public Vector3 PullDirection { get; set; }
    public void SetVisualizePullDirection(Vector3 direction);


}