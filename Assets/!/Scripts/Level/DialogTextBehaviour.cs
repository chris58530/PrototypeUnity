using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogTextBehaviour : PlayableBehaviour
{
    public string dialogText;
    public bool needConfrimToContinue;
    public bool frameShowing;


    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        DialogTextController controller = playerData as DialogTextController;
        float progress = (float)(playable.GetTime() / playable.GetDuration());

        if (!frameShowing) progress = 0.9f;
        controller.TextLineOnUpdate(dialogText, progress);
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        var duration = playable.GetDuration();
        var count = playable.GetTime() + info.deltaTime;

        if ((info.effectivePlayState == PlayState.Paused && count > duration) ||
            playable.GetGraph().GetRootPlayable(0).IsDone())
        {

            if (needConfrimToContinue)
            {
                TimeLineManager.Instance.StopTimeLine();
            }
        }
    }
}