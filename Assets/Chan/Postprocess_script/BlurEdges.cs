using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using System;

public class BlurEdgesRenderer : PostProcessEffectRenderer<BlurEdges>
{
    public override void Render(PostProcessRenderContext context)
    {
        var sheet = context.propertySheets.Get(Shader.Find("Hidden/Custom/BlurEdgesShader"));
        sheet.properties.SetFloat("_Strength", settings.strength);
        context.command.BlitFullscreenTriangle(context.source, context.destination, sheet, 0);
    }
}
[Serializable]
[PostProcess(typeof(BlurEdgesRenderer), PostProcessEvent.AfterStack, "Custom/BlurEdges")]
public class BlurEdges : PostProcessEffectSettings
{
    [Range(0f, 1f), Tooltip("Blur strength")]
    public FloatParameter strength = new FloatParameter { value = 0.5f };
}