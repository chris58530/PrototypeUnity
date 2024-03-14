using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMaterial : MonoBehaviour
{
    public float fadeTime = 3f;
    public List<Transform> objs; 

    private List<Material> originalMaterials = new List<Material>(); 
    private List<Material> newMaterials = new List<Material>(); 
    private List<float> startTime = new List<float>(); 
    private List<float> currentAlpha = new List<float>(); 
    private float targetAlpha = 0f;

    void Start()
    {

        foreach (Transform objs in objs)
        {
            Renderer renderer = objs.GetComponent<Renderer>();
            if (renderer != null)
            {
                originalMaterials.Add(renderer.material);
                newMaterials.Add(new Material(renderer.material));
                startTime.Add(Time.time);
                currentAlpha.Add(renderer.material.color.a);
            }
            else
            {
                Debug.LogWarning("Cant Find Renderer " + objs.name);
            }
        }
    }

    void Update()
    {
        Destroy(gameObject,3f);
        for (int i = 0; i < objs.Count; i++)
        {
            float elapsedTime = Time.time - startTime[i];

            float t = Mathf.Clamp01(elapsedTime / fadeTime);

            Color newColor = newMaterials[i].color;
            newColor.a = Mathf.Lerp(currentAlpha[i], targetAlpha, t);

            newMaterials[i].color = newColor; 
        }
        
    }
}
