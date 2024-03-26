using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogTextClip : PlayableAsset, ITimelineClipAsset
{
    private readonly DialogTextBehaviour _dialogTextBehaviour = new DialogTextBehaviour();
    public string dialogText;

    public ClipCaps clipCaps => ClipCaps.None;

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogTextBehaviour>.Create(graph, _dialogTextBehaviour);
        DialogTextBehaviour clone = playable.GetBehaviour();
        clone.dialogText = this.dialogText;
        return playable;
    }
}