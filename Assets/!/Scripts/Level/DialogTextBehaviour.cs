using System.Collections;
using System.Collections.Generic;
using UnityEngine.Playables;

public class DialogTextBehaviour : PlayableBehaviour
{
    public string dialogText;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        DialogTextController controller = playerData as DialogTextController;
        float progress = (float)(playable.GetTime() / playable.GetDuration());
        controller.TextLineOnUpdate(dialogText, progress);
    }
}